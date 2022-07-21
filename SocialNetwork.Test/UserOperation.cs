using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SocialNetwork.Test
{
    public class UserOperation : IMUserRepository
    {                      
        public int Create(UserEntity userEntity)
        {
            FakeBaseData.Add(userEntity);
            return userEntity.id;
        }
        public UserEntity GetUserEntity(int userId)
        {
            var userEntity = FakeBaseData.Find(t => t.id == userId);
            return (userEntity is null) ?
              throw new UserEntityNotFoundException() : userEntity;
        }
        public int DeleteById(int id)
        {
            var userEntity = FakeBaseData.Find(t => t.id == id); 
            FakeBaseData.Remove(userEntity);
            return id;
        }      
        public UserEntity FindByEmail(string email)
        {
            var userEntity = FakeBaseData.Find(t => t.email.Contains(email));
            return (userEntity is null) ?
              throw new UserEntityNotFoundException() : userEntity;
        }
        public UserEntity FindById(int id)
        {
            var userEntity = FakeBaseData.Find(t => t.id == id);
            return (userEntity is null) ?
              throw new UserEntityNotFoundException() : userEntity;
        }
        public IEnumerable<UserEntity> ListAll()
        {
            return FakeBaseData;
        }
        private List<UserEntity> FakeBaseData = new List<UserEntity>
        {
           new UserEntity {id=1, firstname="Иван", lastname="Иванов", password="10101010", email="ivanov@mail.ru", photo="", favorite_book="",favorite_movie="" },
           new UserEntity {id=2, firstname="Петр", lastname="Петров", password="10101010", email="petrov@mail.ru", photo="", favorite_book="",favorite_movie=""}
        };
    }
    public interface IMUserRepository
    {
        int Create(UserEntity userEntity);
        UserEntity FindByEmail(string email);
        IEnumerable<UserEntity> ListAll();
        UserEntity FindById(int id);       
        int DeleteById(int id);
    }
}
