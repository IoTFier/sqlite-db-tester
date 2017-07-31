﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using IntroToSQLite.Models;

namespace IntroToSQLite.Interfaces
{
    public interface IRandomThoughtDatabase
    {
        IEnumerable<RandomThought> GetThoughts();

        RandomThought GetThought(int id);
        void DeleteThought(int id);
        void AddThought(string thought);
        string GetDBSize(string theType);
        int QueryNumRows();
        void DropTable();
    }
}
