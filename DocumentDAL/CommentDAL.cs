using Contracts;
using DalParametersConverterExpression;
using DocumentContracts.DTO;
using DocumentContracts.DTO.Comment;
using DocumentContracts.Interfaces.Comment;
using InfraDALContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentDALImpl
{
    [Register(Policy.Transient, typeof(ICommentDAL))]
    public class CommentDAL : ICommentDAL
    {
        private IInfraDal _SQLDAL;
        DBParameterConverter _paramConverter;
        private IDBConnection con;
        public CommentDAL(IInfraDal SQLDAL)
        {
            _SQLDAL = SQLDAL;
            _paramConverter = new DBParameterConverter(_SQLDAL);
            con = _SQLDAL.Connect("Server=LAPTOP-B6F4SVRM;Database=DocumentProject;" + "Trusted_Connection=True;");

        }
        public CommentResponse CreateComment(CommentRequest request)
        {

            CommentResponse retval = default;
            try
            {
                var parameters = _paramConverter.ConvertToParameters(request.commentDTO);
                var dataset = _SQLDAL.ExecSPQuery("CreateComment", con, parameters);
                if (dataset.Tables[0].Rows.Count!=0)
                {
                    retval = new CommentResponseAddOK() { comments=new List<CommentDTO>() { request.commentDTO } };
                    return retval;
                }
               
            }
            catch (Exception e)
            {
                retval = new CommentResponseDontAdd();
                //log
            }

            return retval;
        }

        public CommentResponse DeleteComment(CommentRequestDelete request)
        {
            CommentResponse retval = default;
            try
            {
                var parameter = _paramConverter.ConvertToParameter(request ,"CommentID");
                var dataset = _SQLDAL.ExecSPQuery("RemoveComment", con, parameter);
                if (dataset.Tables[0].Rows.Count!=0)
                {
                    retval = new CommentResonseRemoveOk() { comments=new List<CommentDTO>() { new CommentDTO() { CommentID=request.CommentID} } } ;
                }
            }
            catch (Exception e)
            {
                retval = new CommentResponseDontRemove();
                //log
            }

            return retval;
        }
        public CommentResponse GetCommentsForDocument(CommentRequestGetByDocID DocID)
        {
            CommentResponse retval = default;
            try
            {
                var parameter = _paramConverter.ConvertToParameter(DocID,"DocID");
                var dataset = _SQLDAL.ExecSPQuery("GetCommentsForDocument", con, parameter);
                if (dataset != null && dataset.Tables[0].Rows.Count != 0)
                {
                    retval = new CommentResponse()
                    {
                        comments = new List<CommentDTO>()

                    };
                    var commentsList = dataset.Tables[0].AsEnumerable().Select(dataRow => new CommentDTO
                    {

                        CommentID = dataRow.Field<Guid>("CommentID"),
                        MarkerID = dataRow.Field<Guid>("MarkerID"),
                        UserId = dataRow.Field<string>("UserId"),
                        Content = dataRow.Field<string>("Content"),
                        CommentDate = dataRow.Field<DateTime>("CommentDate")


                    }).ToList();
                    retval.comments = commentsList;
                }
            }
            catch (Exception e)
            {
                //log
                throw;
            }
            return retval;
        }
    }
}

