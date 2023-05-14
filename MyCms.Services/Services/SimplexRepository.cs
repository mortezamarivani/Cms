using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Simplex;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Simplex;
using MyCms.ViewModels.St;

namespace MyCms.Services.Services
{
    public class SimplexRepository : ISimplexRepository
    {
        private MyCmsDbContext _db;
        public SimplexRepository(MyCmsDbContext db)
        {
            _db = db;
        }

        IEnumerable<ShowSimplexViewModel> ISimplexRepository.GetAllSimplex(int tblNumber, int stCode)
        {
            var ret = (from simplex in _db.Simplex
                       join variableRow in _db.Variable on simplex.VarNumRow equals variableRow.VarCode
                       join variableCol in _db.Variable on simplex.VarNumCol equals variableCol.VarCode
                       select new  ShowSimplexViewModel{ 
                          SimplexID = simplex.SimplexID,
                          VarNumCol = simplex.VarNumCol,
                          VarNumRow = simplex.VarNumRow,
                          RowIndex = simplex.RowIndex,
                          ColIndex = simplex.ColIndex,
                          ColVarName = variableCol.VarName,
                          RowVarName = variableRow.VarName,
                          InOut = simplex.InOut,
                          TblNumber = simplex.TblNumber,
                          Value = simplex.Value,
                           TypeVar = simplex.TypeVar,
                           StCode = simplex.StCode,
                           Min = simplex.Min,
                           Out = simplex.Out,
                       }).Where(r=> r.StCode == stCode && r.TblNumber == tblNumber ).ToList();

            return ConvertToShowSimplexViewModel(ret);
        }


        IEnumerable<ShowSimplexViewModel> ISimplexRepository.GetAllSimplex(int tblNumber)
        {
            var ret = (from simplex in _db.Simplex
                       join variableRow in _db.Variable on simplex.VarNumRow equals variableRow.VarCode
                       join variableCol in _db.Variable on simplex.VarNumCol equals variableCol.VarCode
                       where(simplex.TblNumber == tblNumber)
                       select new ShowSimplexViewModel
                       {
                           SimplexID = simplex.SimplexID,
                           VarNumCol = simplex.VarNumCol,
                           VarNumRow = simplex.VarNumRow,
                           RowIndex = simplex.RowIndex,
                           ColIndex = simplex.ColIndex,
                           ColVarName = variableCol.VarName,
                           RowVarName = variableRow.VarName,
                           InOut = simplex.InOut,
                           TblNumber = simplex.TblNumber,
                           Value = simplex.Value,
                           TypeVar = simplex.TypeVar,
                           StCode = simplex.StCode,
                           Out = simplex.Out,
                       })
                       .Where(r=> r.StCode == 100)
                       .ToList();

            return ConvertToShowSimplexViewModel(ret);
        }

        List<Simplex> ISimplexRepository.GetSimplexForMin(int tblNumber, int stCode)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.VarNumRow == 88 && r.VarNumCol != 99 && r.Value < 0).ToList();
        }

        List<Simplex> ISimplexRepository.GetSimplexForRhs(int tblNumber, int stCode)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.VarNumCol == 99 && r.VarNumRow != 88)
                .OrderBy(r => r.RowIndex)
                .ToList();
        }

        List<Simplex> ISimplexRepository.GetSimplexForMinColIndexs(int tblNumber, int stCode, int MinColIndex)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.ColIndex == MinColIndex && r.VarNumRow != 88)
                 .OrderBy(r => r.RowIndex)
                 .ToList();
        }


        List<Simplex> ISimplexRepository.GetSimplexForLola(int tblNumber, int stCode,int rowIndexOfLola)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.RowIndex == rowIndexOfLola)
                 .OrderBy(r => r.RowIndex)
                 .ToList();
        }

        List<Simplex> ISimplexRepository.GetSimplexForZ(int tblNumber, int stCode, int varNumRow)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.VarNumRow == varNumRow)
                 .OrderBy(r => r.RowIndex)
                 .ToList();
        }

        double ISimplexRepository.LolaValueZ(int tblNumber, int stCode, int varNumRow, int varNumCol)
        {
            return _db.Simplex.First(r => r.TblNumber == tblNumber && r.StCode == stCode && r.VarNumRow == varNumRow && r.VarNumCol == varNumCol).Value;
                
        }


        public int UpdateSimplexMin(int tblNumber, int stCode , List<Simplex> simplex)
        {
            List<Simplex> retSimplex = new List<Simplex>();
            int simplexId = 0;
            double minValue = 0;
            int MinColIndex = 0;
            bool firstRow = true;
            Simplex updateSimplext = new Simplex();

            retSimplex = simplex;

            foreach (var item in retSimplex)
            {
                if (firstRow)
                {
                    minValue = item.Value;
                    simplexId = item.SimplexID;
                    firstRow = false;
                    MinColIndex = item.ColIndex;

                    updateSimplext.Min = true;
                    updateSimplext.InOut = 1;
                    updateSimplext.ColIndex = item.ColIndex;
                    updateSimplext.RowIndex = item.RowIndex;
                    updateSimplext.StCode = item.StCode;
                    updateSimplext.TblNumber = item.TblNumber;
                    updateSimplext.TypeVar = item.TypeVar;
                    updateSimplext.Value = minValue;
                    updateSimplext.VarNumCol = item.VarNumCol;
                    updateSimplext.VarNumRow = item.VarNumRow;
                    updateSimplext.Out = item.Out;

                }
                else if (item.Value < minValue)
                {
                    minValue = item.Value;
                    simplexId = item.SimplexID;
                    MinColIndex = item.ColIndex;

                    updateSimplext.Min = true;
                    updateSimplext.InOut = 1;
                    updateSimplext.ColIndex = item.ColIndex;
                    updateSimplext.RowIndex = item.RowIndex;
                    updateSimplext.StCode = item.StCode;
                    updateSimplext.TblNumber = item.TblNumber;
                    updateSimplext.TypeVar = item.TypeVar;
                    updateSimplext.Value = minValue;
                    updateSimplext.VarNumCol = item.VarNumCol;
                    updateSimplext.VarNumRow = item.VarNumRow;
                    updateSimplext.Out = item.Out;
                }

            }

            if (simplexId > 0)
            {
                DeleteSimplex(simplexId);
                InsertSimplex(updateSimplext);
                Save();
            }
            return MinColIndex;
        }

        public Simplex UpdateSimplexOutVar(int tblNumber, int stCode , List<Simplex> rhs, List<Simplex> minColIndexs)
        {
            List<Simplex> Rhs = new List<Simplex>();
            List<Simplex> MinColIndexs = new List<Simplex>();
            Simplex OutPutVar;

            int simplexId = 0;
            double Tetta = 0;
            bool firstRow = true;
            Simplex updateSimplext = new Simplex();

            Rhs = rhs;
            MinColIndexs = minColIndexs;

            foreach (var minColIndeItem in MinColIndexs)
            {
                double MinRhsValue = 0;
                double valMin = 0;
                if (firstRow && minColIndeItem.Value > 0)
                {
                    MinRhsValue = (Int32)(Rhs.FirstOrDefault(r => r.RowIndex == minColIndeItem.RowIndex).Value);
                    Tetta = MinRhsValue / minColIndeItem.Value;
                    firstRow = false;

                    simplexId = minColIndeItem.SimplexID;
                    updateSimplext.Min = false;
                    updateSimplext.InOut = 2;//lola
                    updateSimplext.ColIndex = minColIndeItem.ColIndex;
                    updateSimplext.RowIndex = minColIndeItem.RowIndex;
                    updateSimplext.StCode = minColIndeItem.StCode;
                    updateSimplext.TblNumber = minColIndeItem.TblNumber;
                    updateSimplext.TypeVar = minColIndeItem.TypeVar;
                    updateSimplext.Value = minColIndeItem.Value;
                    updateSimplext.VarNumCol = minColIndeItem.VarNumCol;
                    updateSimplext.VarNumRow = minColIndeItem.VarNumRow;
                    updateSimplext.Out = minColIndeItem.Out;

                }
                else if (!firstRow && minColIndeItem.Value > 0 )
                {
                    valMin = (Int32)(Rhs.FirstOrDefault(r => r.RowIndex == minColIndeItem.RowIndex).Value) ;
                    valMin = valMin / minColIndeItem.Value;
                    if (valMin < Tetta)
                    {
                        MinRhsValue = (Int32)(Rhs.FirstOrDefault(r => r.RowIndex == minColIndeItem.RowIndex).Value);
                        Tetta = MinRhsValue / minColIndeItem.Value;

                        simplexId = minColIndeItem.SimplexID;
                        updateSimplext.Min = false;
                        updateSimplext.InOut = 2;//lola
                        updateSimplext.ColIndex = minColIndeItem.ColIndex;
                        updateSimplext.RowIndex = minColIndeItem.RowIndex;
                        updateSimplext.StCode = minColIndeItem.StCode;
                        updateSimplext.TblNumber = minColIndeItem.TblNumber;
                        updateSimplext.TypeVar = minColIndeItem.TypeVar;
                        updateSimplext.Value = minColIndeItem.Value;
                        updateSimplext.VarNumCol = minColIndeItem.VarNumCol;
                        updateSimplext.VarNumRow = minColIndeItem.VarNumRow;
                        updateSimplext.Out = minColIndeItem.Out;
                    }
                }

            }

            OutPutVar = _db.Simplex.First(r => r.TblNumber == tblNumber && r.StCode == stCode && r.RowIndex == updateSimplext.RowIndex && r.ColIndex == updateSimplext.ColIndex);
            if(OutPutVar.SimplexID == simplexId)
            {
                DeleteSimplex(OutPutVar.SimplexID);
                OutPutVar.SimplexID = 0;
                OutPutVar.InOut = 2;//lola
                OutPutVar.Out = true;//out
                InsertSimplex(OutPutVar);
                Save();
            }
            else
            {
                if (OutPutVar != null)
                {
                    DeleteSimplex(OutPutVar.SimplexID);
                    OutPutVar.SimplexID = 0;
                    OutPutVar.Out = true;//out
                    InsertSimplex(OutPutVar);
                    Save();
                }

                if (simplexId > 0)
                {
                    DeleteSimplex(simplexId);
                    InsertSimplex(updateSimplext);
                    Save();
                }
            }

            return OutPutVar;//retturn lola

        }
        public List<ShowSimplexViewModel> ConvertToShowSimplexViewModel(List<ShowSimplexViewModel> Simplex)
        {
            List<ShowSimplexViewModel> RetToSmplx = new List<ShowSimplexViewModel>();
            foreach (var item in Simplex)
            {
                RetToSmplx.Add(new ShowSimplexViewModel()
                {
                    ColIndex = item.ColIndex,
                    ColVarName = item.ColVarName,
                    InOut = item.InOut,
                    RowIndex = item.RowIndex,
                    RowVarName = item.RowVarName,
                    SimplexID = item.SimplexID,
                    TblNumber = item.TblNumber,
                    Value = item.Value,
                    VarNumCol = item.VarNumCol,
                    VarNumRow = item.VarNumRow,
                    TypeVar=item.TypeVar,
                    StCode = item.StCode,
                    Min = item.Min,
                    Out=item.Out
                });
            }


            return RetToSmplx;
        }
        public Simplex GetSimplexByIp(int SimplexID)
        {
            return _db.Simplex.Where(c => c.SimplexID == SimplexID ).FirstOrDefault();
        }


        public bool SimplexExists(int SimplexID)
        {
            return _db.Simplex.Any(c => c.SimplexID == SimplexID);
        }

        public void DeleteSimplex(Simplex Simplex)
        {
            List<Simplex> simplices = new List<Simplex>();
            simplices = _db.Simplex.Select(c => c).Where(r => r.StCode == Simplex.StCode).ToList();
            foreach (Simplex item in simplices)
            {
                _db.Simplex.Remove(item);
                Save();
            }
        }

        public void DeleteSimplex(List<Simplex> Simplexes)
        {
            foreach (Simplex item in Simplexes)
            {
                _db.Simplex.Remove(item);
                Save();
            }
        }

        public void DeleteSimplex(int SimplexId)
        {
            var Simplex = _db.Simplex.Find(SimplexId);
            _db.Simplex.Remove(Simplex);
            Save();

        }

        public IEnumerable<Simplex> GetAllSimplexe()
        {
            return _db.Simplex.Where(r=> r.StCode == 100 ).ToList();
        }

        public void InsertSimplex(Simplex Simplex)
        {
            _db.Simplex.Add(Simplex);
        }

        public void InsertSimplex(List<Simplex> Simplexs)
        {
            foreach (Simplex item in Simplexs)
            {
                _db.Simplex.Add(item);
            }
            
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateSimplex(Simplex Simplex)
        {
            _db.Entry(Simplex).State = EntityState.Modified;
        }

       

        public int GetMaxRowIndex(int tblNumber, int stCode)
        {
            if (_db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode).Select(r => r.RowIndex).Count() > 0)
                return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode).Select(r => r.RowIndex).Max();
            return 0;
        }

        public int GetCountTableOfSimplex(int stCode)
        {
            if (_db.Simplex.Where(r => r.StCode == stCode).Select(r => r.TblNumber).Count()>0)
                return _db.Simplex.Where(r => r.StCode == stCode).Select(r => r.TblNumber).Max();

            return 0;
        }

        public int GetMaxColIndex(int tblNumber , int stCode)
        {
            if (_db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode).Select(r => r.ColIndex).Count() > 0 )
                return  _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode).Select(r => r.ColIndex).Max();
            return 0;
        }

        public int GetVarNumRowOfSimplex(int tblNumber, int stCode)
        {
            return _db.Simplex.First(r => r.TblNumber == tblNumber && r.StCode == stCode && r.Out == true).VarNumRow;
        }


        public List<Simplex> GetAnotherSimplexRows(int tblNumber, int stCode, int varNumRow)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.VarNumRow != varNumRow && r.VarNumRow != 88 )
                 .OrderBy(r => r.RowIndex)
                 .ToList();
        }

        public List<Simplex> UpDownLolaColList(int tblNumber, int stCode, int lolaRowindex, int lolaColIndex)
        {
            return _db.Simplex.Where(r => r.TblNumber == tblNumber && r.StCode == stCode && r.ColIndex == lolaColIndex).ToList();
        }


    }
}
