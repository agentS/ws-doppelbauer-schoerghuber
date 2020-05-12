using GraphOverflow.Dtos.Input;
using GraphQL.Types;

namespace GraphOverflow.GraphQl.InputGraphTypes
{
    public class QuestionInputGraphType : InputObjectGraphType<QuestionInputDto>
    {
        public QuestionInputGraphType()
        {
            Field<NonNullGraphType<StringGraphType>>(name: "title");
            Field<NonNullGraphType<StringGraphType>>(name: "content");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>>(name: "tags");
        }
    }
}