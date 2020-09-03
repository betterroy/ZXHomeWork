using HomeWork.Class;
using System;

namespace HomeWork.DB.InterFace
{
    public interface IDBHelper
    {
        void query();
        Company Find( int id );
    }
}
