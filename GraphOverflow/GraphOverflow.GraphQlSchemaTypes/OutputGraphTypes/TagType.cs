using GraphOverflow.Dtos;
using GraphQL.Types;

namespace GraphOverflow.GraphQl.OutputGraphTypes
{
  public class TagType : ObjectGraphType<TagDto>
  {
    #region Construction
    public TagType()
    {
      Name = "Tag";
      InitializeFields();
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("name");
    }
    #endregion Construction
  }
}
