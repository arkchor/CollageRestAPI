using System;
using System.Linq;
using CollageRestAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollageRestApi.Tests
{
    [TestClass]
    public class RemovingFromDbTest
    {
        [TestInitialize]
        public void Init()
        {
            InitialIndex = 109500;
        }
        public int InitialIndex { get; set; }

        [TestMethod]
        public void RemoveFromDb()
        {
            BaseRepository.Instance.GradesCollection.DeleteAll();
            BaseRepository.Instance.CoursesCollection.DeleteAll();
            BaseRepository.Instance.StudentsCollection.DeleteAll();

            Assert.AreEqual(0, BaseRepository.Instance.GradesCollection.Count());
            Assert.AreEqual(0, BaseRepository.Instance.StudentsCollection.Count());
            Assert.AreEqual(0, BaseRepository.Instance.CoursesCollection.Count());
        }

        [TestMethod]
        public void ResetCurrentIndex()
        {
            var indexConfig = BaseRepository.Instance.CurrentIndexConfig.First();
            indexConfig.CurrentIndex = InitialIndex;
            BaseRepository.Instance.CurrentIndexConfig.Update(indexConfig);
            Assert.AreEqual(InitialIndex, BaseRepository.Instance.CurrentIndexConfig.First().CurrentIndex);
        }
    }
}
