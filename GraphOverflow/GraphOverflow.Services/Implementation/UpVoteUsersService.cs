using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphOverflow.Dal;
using GraphOverflow.Domain;
using GraphOverflow.Dtos;

namespace GraphOverflow.Services.Implementation
{
    public class UpVoteUsersService : IUpVoteUsersService
    {
        private readonly IAnswerDao answerDao;

        public UpVoteUsersService(IAnswerDao answerDao)
        {
            this.answerDao = answerDao;
        }
        
        public async Task<IEnumerable<UpVoteUserDto>> FindUpVoteUsersForAnswer(AnswerDto answer)
        {
            return MapUpVoteUsers(await answerDao.FindUpVoteUsersForPost(answer.Id));
        }

        public async Task<IEnumerable<UpVoteUserDto>> FindUpVoteUsersForQuestion(QuestionDto question)
        {
            return MapUpVoteUsers(await answerDao.FindUpVoteUsersForPost(question.Id));
        }

        private IEnumerable<UpVoteUserDto> MapUpVoteUsers(IEnumerable<UpVoteUser> upVoteUsers)
        {
            return upVoteUsers.Select(MapUpVoteUser).ToList();
        }

        private UpVoteUserDto MapUpVoteUser(UpVoteUser upVoteUser)
        {
            var dto = new UpVoteUserDto
            {
                Id = upVoteUser.Id,
                Name = upVoteUser.Name
            };
            return dto;
        }
    }
}