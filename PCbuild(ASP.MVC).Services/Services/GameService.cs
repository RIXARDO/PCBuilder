﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Services.DTO;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Domain.Entities;
using AutoMapper;

namespace PCbuilder_ASP.MVC_.Services.Services
{
    public class GameService : IGameService, IDisposable
    {
        private IGenericRepository<Game> Games { get; set; }
        private IUnitOfWork uow { get; set; }
        private IMapper mapper;

        public GameService(IUnitOfWork unitOfWork, IGenericRepository<Game> repository)
            //IMapper mapper
        {
            uow = unitOfWork;
            Games = repository;
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Game, GameDTO>();
                cfg.CreateMap<GameDTO, Game>();
            }));
        }

        public void DeleteGame(Guid guid)
        {
            var game = Games.FindById(guid);
            Games.Delete(game);
        }

        public void EditGame(GameDTO gamedto)
        {
            Game game = mapper.Map<GameDTO, Game>(gamedto);
            Games.Update(game);
            uow.Save();
        }

        public GameDTO GetGameByID(Guid guid)
        {
            Game game = Games.FindById(guid);
            GameDTO gamedto = mapper.Map<Game, GameDTO>(game);
            return gamedto;
        }

        public IEnumerable<GameDTO> GetGames()
        {
            return mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(Games.Get());
        }

        public void SaveGame(GameDTO gamedto)
        {
            Game game = mapper.Map<GameDTO, Game>(gamedto);
            Games.Create(game);
            uow.Save();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    uow.Save();
                    uow.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Get image from game
        /// </summary>
        /// <param name="guid">Game id</param>
        /// <returns>ImageFile</returns>
        public FileDTO GetImage(Guid guid)
        {
            Game game = Games.FindById(guid);
            if (game != null & game.ImageMimeType64 != null)
            {
                    return new FileDTO {
                        File = game.ImageData64, FileType = game.ImageMimeType64 };
            }
            else
            {
                return null;
            }
        }

    }
}
