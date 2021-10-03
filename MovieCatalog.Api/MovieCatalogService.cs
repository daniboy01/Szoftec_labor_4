﻿using MovieCatalog.Data;
using MovieCatalog.Data.Entities;
using MovieCatalog.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Api
{
    public class MovieCatalogService
    {
        //XWSW5U
        private readonly IMovieCatalogDataService service;

        public MovieCatalogService(IMovieCatalogDataService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await service.GetGenresAsync();

            return genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name });
        }

        public async Task<GenreDto> GetGenreAsync(int id)
        {
            var genres = await service.GetGenresAsync();
            var genre = genres.Where(g => g.Id == id)
                .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                .FirstOrDefault();
            return genre;
        }

        public async Task<GenreDto> InserOrUpdateGenreAsync(int id, string name)
        {
            var genre = await service.InsertOrUpdateGenreAsync(id, name);
            return new GenreDto { Id = genre.Id, Name = genre.Name };
        }

        public async Task<GenreDto> CreateNewGenreAsync(string name)
        {
            var genre = await service.InsertOrUpdateGenreAsync(null, name);
            return new GenreDto { Id = genre.Id, Name = genre.Name };
        }

        public async Task DeleteGenreAsync(int id)
        {
            await service.DeleteGenreAsync(id);
        }

        //XWSW5U

        public async Task<TitleDto> GetTitleById(int id)
        {
            var title = await service.GetTitleByIdAsync(id);

            return mapTitleToDto(title);            
        }

        private TitleDto mapTitleToDto(Title title)
        {
            var dto = new TitleDto
            {
                Id = title.Id,
                TConst = title.TConst,
                PrimaryTitle = title.PrimaryTitle,
                OriginalTitle = title.OriginalTitle,
                TitleType = title.TitleType.ToString(),
                StartYear = title.StartYear,
                EndYear = title.EndYear,
                RuntimeMinutes = title.RuntimeMinutes,
                AverageRating = title.AverageRating,
                NumberOfVotes = title.NumberOfVotes,
                GenreIds = title.Genres.Select(g => g.Id).ToList()
            };

            return dto;
        }

    }
}

