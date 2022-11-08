using AutoMapper;
using Beca.AlbumInfo.API.DbContexts;
using Beca.AlbumInfo.API.Entities;
using Beca.AlbumInfo.API.Models;
using Beca.AlbumInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albumes.Test
{
    public class AlbumesServiciosTest
    {
        // Validar que el album tiene al menos 1 canción
        [Fact]
        public void album_validarCanciones()
        {
            // Arrange

            AlbumDto albumCreado = new AlbumDto()
            {
                Title = "album de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act
            CancionDto cancion = new CancionDto()
            {
                Title = "cancion de ejemplo",
                Description = "Descripción de ejemplo"
            };

            albumCreado.Canciones.Add(cancion);

            var Canciones = albumCreado.Canciones;

            int numeroCanciones = Canciones.Count();

            // Assert
            Assert.True(numeroCanciones > 0);
        }
    }
}
