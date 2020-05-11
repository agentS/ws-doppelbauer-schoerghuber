using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOverflow.Dtos;

namespace GraphOverflow.Services
{
    public interface IUpVoteUsersService
    {
        Task<IEnumerable<UpVoteUserDto>> FindUpVoteUsersForAnswer(AnswerDto answer);
        Task<IEnumerable<UpVoteUserDto>> FindUpVoteUsersForQuestion(QuestionDto question);
    }
}