using Beca.AlbumInfo.API.Entities;
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
    public class CancionesTest
    {
        // Validar que el nombre de la canción tiene más de 1 caracter
        [Fact]
        public void crearCancion_validarNombre()
        {
            // Arrange
            CancionForCreationDto cancionCreada = new CancionForCreationDto()
            {
                Title = "cancion de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act
            var longCadena = cancionCreada.Title.Length;

            // Assert
            Assert.True(1 < longCadena);
        }

        // Validar que la descripción del album tiene más de 1 caracter
        [Fact]
        public void crearCancion_validarDescripcion()
        {
            // Arrange
            CancionForCreationDto cancionCreada = new CancionForCreationDto()
            {
                Title = "cancion de ejemplo",
                Description = "descripción de ejemplo"
            };

            // Act
            var longCadena = cancionCreada.Description.Length;

            // Assert
            Assert.True(300 > longCadena);
        }

        // Comprobar que el título contiene x texto
        [Fact]
        public void crearCancion_comprobarTitulo()
        {
            // Arrange
            CancionForCreationDto cancionCreada = new CancionForCreationDto()
            {
                Title = "titulo",
                Description = "descripción de ejemplo"
            };

            // Act
            cancionCreada.Title = "prueba de cancion";

            // Assert
            Assert.Contains("canci", cancionCreada.Title);
        }

    }
}
