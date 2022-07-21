using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.BLL.Services
{
    public class FriendService
    {
        IFriendRepository friendRepository;
        IUserRepository userRepository;
        public FriendService()
        {
            friendRepository = new FriendRepository();
            userRepository = new UserRepository();
        }
        public IEnumerable<Friend> GetFriendsByUserId(int userId)
        {
            var friends = new List<Friend>();

            friendRepository.FindAllByUserId(userId).ToList().ForEach(m =>
            {
                var userUserEntity = userRepository.FindById(m.user_id);
                var friendUserEntity = userRepository.FindById(m.friend_id);

                friends.Add(new Friend(m.id, userUserEntity.email, friendUserEntity.email));
            });

            return friends;
        }
        public void AddFriend(FriendData friendData)
        {
            if (String.IsNullOrEmpty(friendData.FriendEmail))
                throw new ArgumentNullException();

            var findUserEntity = this.userRepository.FindByEmail(friendData.FriendEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                user_id = friendData.UserId,
                friend_id = findUserEntity.id
            };
            var mfriendEntity = this.friendRepository.FindByFriendEntity(friendEntity);
            if (!(mfriendEntity is null)) throw new FriendExistException();

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }
    }
}
