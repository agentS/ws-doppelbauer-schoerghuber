using GraphOverflow.Dtos;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class AnswerType : ObjectGraphType<AnswerDto>
  {
    #region Members
    #endregion Members

    #region Construction
    public AnswerType()
    {
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Answer";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<IntGraphType>>("upVoats");
      Field<NonNullGraphType<QuestionType>>(
        name: "question", resolve: ResolveQuestion);
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<CommentType>>>>(
        name: "comments", resolve: ResolveComments);

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers
    private object ResolveComments(IResolveFieldContext<AnswerDto> arg)
    {
      throw new NotImplementedException();
    }

    private object ResolveQuestion(IResolveFieldContext<AnswerDto> arg)
    {
      throw new NotImplementedException();
    }
    #endregion Resolvers
  }
}
