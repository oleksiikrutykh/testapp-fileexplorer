using  Dxo.FileExplorer.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dxo.FileExplorer.ViewModels.Common
{
    class ErrorMessagePresenter : ErrorPresenter<string>
    {
        public const string GenericError = "Generic";

        public const string FileNotFoundError = "FileNotFound";

        public const string DirectoryNotFoundError = "FileNotFound";

        public ErrorMessagePresenter()
        {
            AddCustomCase(GenericError, ex => LocalizedStrings.Error_GenericError);

            AddCustomCase(FileNotFoundError,
                          ex => LocalizedStrings.Error_FileNotExistError, 
                          ex => ex is FileNotFoundException);

            AddCustomCase(DirectoryNotFoundError,
                          ex => LocalizedStrings.Error_FolderNotExist,
                          ex => ex is DirectoryNotFoundException);


        }
    }

    class ErrorPresenter<T>
    {
        

        private List<ErrorCase<T>> _cases = new List<ErrorCase<T>>();

        public IReadOnlyList<ErrorCase<T>> Cases { get => _cases; }


        public void AddCustomCase(string name, 
                                    Func<Exception, T> processingDelegate,
                                    Func<Exception, bool> canProcess = null,
                                    int insertionIndex = 0)
        {
            var item = new ErrorCase<T>(name, processingDelegate, canProcess);
            AddCustomCase(item, insertionIndex);
        }

        public void AddCustomCase(ErrorCase<T> errorCase, int insertionIndex = 0)
        {
            _cases.Insert(0, errorCase);
        }

        public void RemoveCase(string caseName)
        {
            _cases.RemoveAll(c => c.Name == caseName);
        }

        public T ConvertError(Exception ex)
        {
            foreach (var handler in _cases)
            {
                if (handler.CanProcess(ex))
                {
                    return handler.Process(ex);
                }
            }

            return default(T);
        }

        
    }

    class ErrorCase<T>
    {
        public ErrorCase(string name, Func<Exception, T> processingFunc, Func<Exception, bool> canProcess)
        {
            Name = name;
            ProcessingFunc = processingFunc;
            CanProcessFunc = canProcess ?? new Func<Exception, bool>(ex => true);
        }

        public string Name { get; set; }

        public Func<Exception, bool> CanProcessFunc { get; set; }

        public Func<Exception, T> ProcessingFunc { get; set; }

        public virtual T Process(Exception ex)
        {
            return ProcessingFunc.Invoke(ex);
        }

        public virtual bool  CanProcess(Exception ex)
        {
            return CanProcessFunc.Invoke(ex);
        }
    }
}
