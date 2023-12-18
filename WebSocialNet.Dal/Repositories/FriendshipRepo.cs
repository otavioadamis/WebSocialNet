using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.DTOs.FriendshipDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;

namespace WebSocialNet.Dal.Repositories
{
    public class FriendshipRepo : IFriendshipRepo
    {
        private readonly AppDbContext _dbContext;
        public FriendshipRepo(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        // Save changes (?)
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Friendship Add(Friendship thisFriendship)
        {
            _dbContext.Friendships.Add(thisFriendship);
            return thisFriendship;
        }

        //todo -> query que retorna todos os amigos por ordem de criacao (inversa)
        public IEnumerable<FriendshipDTO> GetAllFriendsFromUserId(string userId)
        {
            var friendships = from friendship in _dbContext.Friendships
                              where friendship.UserId == userId
                              join friend in _dbContext.Users on friendship.FriendId equals friend.Id
                              select new FriendshipDTO
                              {
                                  FriendEmail = friend.Email,
                                  FriendName = friend.Name,
                                  FriendsAt = friendship.FriendsAt
                              };
            return friendships;                     
        }

        public void Delete(string _id)
        {
            var friendship = _dbContext.Friendships.FirstOrDefault(x => x.FriendshipId == _id);
            _dbContext.Friendships.Remove(friendship);
        }
    }
}
