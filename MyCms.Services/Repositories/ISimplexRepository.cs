using MyCms.DomainClasses.Polls;
using MyCms.DomainClasses.Simplex;
using MyCms.ViewModels.Simplex;
using System;
using System.Collections.Generic;

namespace MyCms.Services.Repositories
{
    public interface ISimplexRepository
    {
        IEnumerable<ShowSimplexViewModel> GetAllSimplex(int tblNumber, int stCode);
        IEnumerable<ShowSimplexViewModel> GetAllSimplex(int tblNumber);
        int GetMaxRowIndex(int tblNumber, int stCode);
        int GetMaxColIndex(int tblNumber, int stCode);
        void InsertSimplex(Simplex Simplex);
        void InsertSimplex(List<Simplex> Simplexs);
        void UpdateSimplex(Simplex Simplex);
        int UpdateSimplexMin(int tblNumber, int stCode, List<Simplex> simplex);
        Simplex UpdateSimplexOutVar(int tblNumber, int stCode, List<Simplex> rhs, List<Simplex> minColIndexs);
        void DeleteSimplex(Simplex Simplex);
        void DeleteSimplex(List<Simplex> Simplexes);

        bool SimplexExists(int SimplexID);
        Simplex GetSimplexByIp(int SimplexID);
        void Save();

        List<Simplex> GetSimplexForMin(int tblNumber, int stCode);

        List<Simplex> GetSimplexForRhs(int tblNumber, int stCode);

        List<Simplex> GetSimplexForMinColIndexs(int tblNumber, int stCode, int MinColIndex);
        List<Simplex> GetSimplexForLola(int tblNumber, int stCode, int rowIndexOfLola);
        List<Simplex> GetSimplexForZ(int tblNumber, int stCode, int varNumRow);

        double LolaValueZ(int tblNumber, int stCode, int varNumRow, int varNumCol);

        int GetCountTableOfSimplex(int stCode);

        int GetVarNumRowOfSimplex(int tblNumber, int stCode);
        List<Simplex> GetAnotherSimplexRows(int tblNumber, int stCode, int varNumRow);

        List<Simplex> UpDownLolaColList( int tblNumber, int stCode, int lolaRowindex , int lolaColIndex );
        

    }
}
