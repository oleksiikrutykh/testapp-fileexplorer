using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.Core.Services;
using TestApp.FileExplorer.ViewModels;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Tests.ViewModels
{
    [TestFixture]
    public class FoldersListViewModelTest
    {
        [Test]
        public async Task TestNormalLoadingFlow()
        {
            const int foldersCount = 10;
            var fileServiceMock = new Mock<IFileBrowserService>();
            fileServiceMock.Setup(s => s.GetFolders())
                           .Returns(new Func<Task<List<IFolder>>>(async () =>
                             {
                                 await Task.Delay(100);
                                 var items = new List<IFolder>();
                                 for (int i = 0; i < foldersCount; i++)
                                 {
                                     var objectMock = new Mock<IFolder>();
                                     items.Add(objectMock.Object);
                                 }

                                 return items;
                             }));

            var regionManagerMock = new Mock<IRegionManager>();
            var taskSource = new TaskCompletionSource<bool>();
            var vm = new FoldersListViewModel(fileServiceMock.Object, regionManagerMock.Object);
            vm.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(FoldersListViewModel.Data))
                {
                    // Give chanse to update all relative properties.
                    await Task.Delay(10);
                    taskSource.TrySetResult(true);
                }
            };

            await Task.WhenAny(taskSource.Task, Task.Delay(2000));
            Assert.That(vm.Data.Count, Is.EqualTo(10));
            Assert.That(!vm.EmptyState.IsActive);
            Assert.That(!vm.ErrorState.IsActive);
            Assert.That(!vm.LoadingState.IsActive);
            Assert.That(vm.SelectedFolder, Is.Not.Null);
        }
    }
}
