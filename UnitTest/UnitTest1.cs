using aaaaaa;
using aaaaaa.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{

    public interface IMoviesCatalog
    {
        IEnumerable<Movies> Movies { get; }
    }

    public class MoviesCatalog : IMoviesCatalog
    {
        public IEnumerable<Movies> Movies
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class Getter
    {
        private IMoviesCatalog _moviesContainer = null;

        public Getter(IMoviesCatalog productsContainer)
        {
            _moviesContainer = productsContainer;
        }

        public Movies GetMovie(string title)
        {
            return _moviesContainer.Movies.Where(t => t.Title == title).FirstOrDefault();
        }

        public Type GetTypeD()
        {
            return _moviesContainer.Movies.FirstOrDefault().GetType();
        }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange 
            Mock<IMoviesCatalog> mock = new Mock<IMoviesCatalog>();
            mock.Setup(m => m.Movies).Returns(new Movies[] {
            new Movies { Title = "Rambo"},
            new Movies { Title = "Avengers"}
            });
            Getter getter = new Getter(mock.Object);
            // Act
            var result = getter.GetMovie("Rambo");
            // Assert
            Assert.AreEqual("Rambo", result.Title);
            Assert.AreNotEqual("Avatar", result.Title);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var controler = new MoviesController();
            Mock<IMoviesCatalog> mock = new Mock<IMoviesCatalog>();
            mock.Setup(m => m.Movies).Returns(new Movies[] {
            new Movies { Title = "Rambo"}
            });

            Getter getter = new Getter(mock.Object);

            //var r = controler.Info(  ) as ViewResult;

            Assert.AreEqual("Info", r.ViewName);
            Assert.IsInstanceOfType(r.Model, typeof(Movies));
        }
    }
}
