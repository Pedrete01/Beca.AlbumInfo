﻿using Beca.AlbumInfo.API.Entities;
using Beca.AlbumInfo.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Albumes.Test
{
    public class AlbumesTest
    {
        // Validar que el nombre del album tiene más de 1 caracter
        [Fact]
        public void crearAlbum_validarNombre()
        {
            // Arrange
            AlbumForCreationDto albumCreado = new AlbumForCreationDto()
            {
                Title = "album de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act
            var longCadena = albumCreado.Title.Length;

            // Assert
            Assert.True(1 < longCadena);
        }

        // Validar que la descripción del album tiene más de 1 caracter
        [Fact]
        public void crearAlbum_validarDescripcion()
        {
            // Arrange
            AlbumForCreationDto albumCreado = new AlbumForCreationDto()
            {
                Title = "album de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act
            var longCadena = albumCreado.Description.Length;

            // Assert
            Assert.True(300 > longCadena);
        }

        // Comprobar que el título contiene x texto
        [Fact]
        public void crearAlbum_comprobarTitulo()
        {
            // Arrange
            AlbumForCreationDto albumCreado = new AlbumForCreationDto()
            {
                Title = "album de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act

            // Assert
            Assert.Contains("albu", albumCreado.Title);
        }
    }
}
