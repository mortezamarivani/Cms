using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.DomainClasses.St;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Simplex;
using MyCms.ViewModels.St;
using MyCms.DomainClasses.Simplex;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Areas.Simplex.Controllers
{
    [Area("Simplex")]
    public class SimplexController : Controller
    {
        #region Constractor
        private ISimplexRepository _db;
        private IStRepository _dbSt;
        private int NonBaseVarCount;
        private int CountCol;
        public SimplexController(ISimplexRepository db, IStRepository dbSt)
        {
            _db = db;
            _dbSt = dbSt;
            NonBaseVarCount = 0;
            CountCol = 0;
        }

        #endregion
        // GET: Login/UserXes
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShowSimplexViewModel> Simplex = getAllSimplex(1,100);

            ViewBag.StCode = GetSt().StCode;
            ViewBag.StValue = GetSt().StValue;
            string StValueLiner = GetSt().StValueLiner;
            ViewBag.StValueLiner = StValueLiner;

            string[] ret = new string[3];
            int CountRowOfStValueLiner = GetCountRowOfStValueLiner(StValueLiner);
            string[,] StValueLinerArrya = new string[CountRowOfStValueLiner, 4];
            StValueLinerArrya = RetStValueLiner(StValueLiner);
            ViewBag.StValueLinerArrya = StValueLinerArrya;
            ViewBag.CountRowOfStValueLiner = CountRowOfStValueLiner;

            GetIndexes(Simplex);

            return View("Simplex", Simplex);
        }

        private int GetCountRowOfStValueLiner(string StValueLiner)
        {
            int count = 0;
            foreach (var item in StValueLiner.Split("\n"))
            {
                if(item != "")
                    count = count + 1;
            }
            return count;
        }

        public string[,] RetStValueLiner(string strvalue)
        {
            string[,] ret = new string[GetCountRowOfStValueLiner(strvalue),4];
            int indexEq = 0;
            int indexOp = 0;

            int rowId = 0;

            foreach (var item in strvalue.Split("\n"))
            {
                if (rowId ==0 && item != "")
                {
                    ret[rowId, 0] = "Z";
                    ret[rowId, 1] = item.Substring(1 , item.IndexOf("=")-1);
                    ret[rowId, 2] = "=";
                    ret[rowId, 3] = "0";
                    rowId = rowId + 1;
                }
                else if (rowId > 0 && item != "")
                {
                    indexEq = item.IndexOf("=");
                    indexOp = GetindexOp(item.Substring(0, indexEq));
                    ret[rowId, 0] = item.Substring(0, indexOp+1);
                    ret[rowId, 1] = item.Substring(indexOp+1, indexEq- indexOp -1 );
                    ret[rowId, 2] = "=";
                    ret[rowId, 3] = item.Substring(indexEq+1, item.Length- indexEq-1);

                    rowId = rowId + 1;
                }
                else
                {
                    rowId = rowId + 1;
                }

            }
          
            return ret;
        }
        #region GetIndexes

        private int GetindexOp(string value)
        {
            int index = 0;
            for (int i = value.Length; i > 0 ; i--)
            {
                if (value.Substring(i-1 , 1) == "+" || value.Substring(i-1, 1) == "-")
                {
                    index = i - 1;
                    return index;
                }
                    
            }
            return index;
        }
        public void GetIndexes(IEnumerable<ShowSimplexViewModel> simplex)
        {
            IEnumerable<ShowSimplexViewModel> Simplexes = null;

            int MaxColIndex = _db.GetMaxColIndex(1, 100);
            int MaxRowIndex = _db.GetMaxRowIndex(1, 100);
            int CountTble = _db.GetCountTableOfSimplex(100);

            ViewBag.MaxColIndex = MaxColIndex;
            ViewBag.MaxRowIndex = MaxRowIndex;
            int[,,] indexOfMinVar = new int[1, 2, CountTble];
            int[,,] indexOfInputVar = new int[1, 2, CountTble];
            int[,,] indexOfOutVar = new int[1, 2, CountTble];
            int[,,] indexOfLolaVar = new int[1, 2, CountTble];

            string[,,] simplexArray = new string[MaxRowIndex + 1, MaxColIndex + 1, CountTble];
            string[,,] simplexArrayZ = new string[MaxRowIndex + 1, MaxColIndex + 1, CountTble];
            //string[,,] simplexArrayRhs = new string[MaxRowIndex + 1, MaxColIndex + 1, CountTble];
            string[,] simplexArrayRhs = new string[MaxColIndex + 1, CountTble];



            for (int i = 0; i < CountTble; i++)
            {
                Simplexes = getAllSimplex(i+1, 100);
                foreach (var item in Simplexes)
                {
                    if (item.Min == true)
                    {
                        indexOfMinVar[0, 0,i] = item.RowIndex;
                        indexOfMinVar[0, 1,i] = item.ColIndex;
                    }

                    if (item.Out == true)
                    {
                        indexOfOutVar[0, 0,i] = item.RowIndex;
                        indexOfOutVar[0, 1,i] = item.ColIndex;
                    }

                    if (item.InOut == 1) // in
                    {
                        indexOfInputVar[0, 0,i] = item.RowIndex;
                        indexOfInputVar[0, 1,i] = item.ColIndex;
                    }
                    else if (item.InOut == 2)//lola
                    {
                        indexOfLolaVar[0, 0,i] = item.RowIndex;
                        indexOfLolaVar[0, 1,i] = item.ColIndex;
                    }

                    simplexArray[item.RowIndex, item.ColIndex, i] = item.Value.ToString();
                    if (item.RowIndex == 1)
                        simplexArray[0, item.ColIndex, i] = item.ColVarName;

                    if (item.ColIndex == 1)
                        simplexArray[item.RowIndex, 0, i] = item.RowVarName;

                    //begin  Zj-Cj  
                    if (item.VarNumRow == 88 && item.VarNumCol != 99 )
                        simplexArrayZ[i, item.ColIndex, i] =item.ColVarName.ToString() + " = "+ item.Value.ToString();
                    // End Zj-Cj  

                    //begin  Rhs نقاط بهینه  
                    if (item.VarNumCol == 99 && item.VarNumRow != 88)
                        simplexArrayRhs[item.VarNumRow , i] = item.RowVarName.ToString() + " = " + item.Value.ToString();
                    // End Rhs  

                    
                }
            }

            for (int tblId =0; tblId < CountTble; tblId++)
            {
                for (int  colId= 0; colId < MaxColIndex; colId++)
                {
                    if (simplexArrayRhs[colId, tblId] == null)
                        simplexArrayRhs[colId, tblId] = "X" + colId + "= 0";
                }
            }
          
            ViewBag.indexOfMinVar = indexOfMinVar;
            ViewBag.indexOfInputVar = indexOfInputVar;
            ViewBag.indexOfOutVar = indexOfOutVar;

            ViewBag.indexOfLolaVar = indexOfLolaVar;

            ViewBag.simplexArray = simplexArray;
            ViewBag.countTble = CountTble;
            ViewBag.simplexArrayZ = simplexArrayZ;
            ViewBag.simplexArrayRhs = simplexArrayRhs;


        }
        #endregion

        [HttpPost]
        public IActionResult AddSt([Bind("StValue,StCode,IsZ")] St st)
        {
            if (st.IsZ == null)
                st.IsZ = false;

            string Z = st.StValue.Trim().Split('\r')[0];
            Int16 CountRow = Convert.ToInt16(st.StValue.Trim().Split('\r').Length);


            string StrValue = null;
            int MaxVariable = 0;

            StrValue = ConvertToLinerValue(CountRow, st.StValue);
            MaxVariable = GetMaxVariable(CountRow, StrValue);
            NonBaseVarCount = MaxVariable;

            string RetStrValue = null;
            RetStrValue = AddBaseVariable(CountRow, StrValue, MaxVariable);//Variable

            List<MyCms.DomainClasses.Simplex.Simplex> simplices = new List<MyCms.DomainClasses.Simplex.Simplex>();
            simplices = GetListVariable(CountRow, RetStrValue, MaxVariable, CountCol);

            
            st.StValueLiner = RetStrValue;

            st.StCode = 100;
            if (ModelState.IsValid)
            {
                _dbSt.InsertSt(st);
                _dbSt.Save();
            }

            int tblNumber = 1;
            double LolaValue = 0;
            int lolaRowindex = 0;
            int LolaVarNumCol = 0;
            int lolaColIndex = 0;
            MyCms.DomainClasses.Simplex.Simplex CurrentLolaSimplex;

            List<MyCms.DomainClasses.Simplex.Simplex> GetSimplexForRhs = new List<MyCms.DomainClasses.Simplex.Simplex>();
            List<MyCms.DomainClasses.Simplex.Simplex> GetSimplexForMinColIndexs = new List<MyCms.DomainClasses.Simplex.Simplex>();

            List<MyCms.DomainClasses.Simplex.Simplex> LolaSimplexs = new List<DomainClasses.Simplex.Simplex>();
            List<MyCms.DomainClasses.Simplex.Simplex> NewSimplexs = new List<DomainClasses.Simplex.Simplex>();

            List<MyCms.DomainClasses.Simplex.Simplex> OldSimplexsZ = new List<DomainClasses.Simplex.Simplex>();
            List<MyCms.DomainClasses.Simplex.Simplex> NewSimplexsLola = new List<DomainClasses.Simplex.Simplex>();

            List<MyCms.DomainClasses.Simplex.Simplex> anotherSimplexRow = new List<DomainClasses.Simplex.Simplex>();
            List<MyCms.DomainClasses.Simplex.Simplex> anotherSmplxObj = new List<DomainClasses.Simplex.Simplex>();

            List<MyCms.DomainClasses.Simplex.Simplex> smplxZ = new List<DomainClasses.Simplex.Simplex>();

            List<MyCms.DomainClasses.Simplex.Simplex> GetSimplexForMin = new List<MyCms.DomainClasses.Simplex.Simplex>();

            do
            {
                GetSimplexForMin = _db.GetSimplexForMin(tblNumber, 100);
                if (GetSimplexForMin.Count > 0)
                {
                    int MinColIndex = _db.UpdateSimplexMin(tblNumber, 100, GetSimplexForMin);
                    GetSimplexForRhs = _db.GetSimplexForRhs(tblNumber, 100);
                    GetSimplexForMinColIndexs = _db.GetSimplexForMinColIndexs(tblNumber, 100, MinColIndex);

                    //process for Lola Row
                    CurrentLolaSimplex = _db.UpdateSimplexOutVar(tblNumber, 100, GetSimplexForRhs, GetSimplexForMinColIndexs);
                    lolaRowindex = CurrentLolaSimplex.RowIndex;
                    lolaColIndex = CurrentLolaSimplex.ColIndex;
                    LolaValue = CurrentLolaSimplex.Value;
                    LolaVarNumCol = CurrentLolaSimplex.VarNumCol;

                    LolaSimplexs = _db.GetSimplexForLola(tblNumber, 100, lolaRowindex);
                    NewSimplexs.Clear();
                    foreach (var simplexItem in LolaSimplexs)
                    {
                        MyCms.DomainClasses.Simplex.Simplex obj = new DomainClasses.Simplex.Simplex();
                        obj.ColIndex = simplexItem.ColIndex;
                        obj.InOut = 0;
                        obj.Min = false;
                        obj.Out = false;
                        obj.RowIndex = simplexItem.RowIndex;
                        obj.StCode = 100;
                        obj.TblNumber = simplexItem.TblNumber + 1;
                        obj.TypeVar = 0;
                        obj.VarNumCol = simplexItem.VarNumCol;
                        obj.VarNumRow = LolaVarNumCol;
                        obj.Value = Convert.ToDouble(string.Format("{0:0.000}", simplexItem.Value / LolaValue));


                        NewSimplexs.Add(obj);
                    };
                    _db.InsertSimplex(NewSimplexs);
                    _db.Save();
                    // End Process Lola Row

                    //Process Z Row

                    OldSimplexsZ = _db.GetSimplexForZ(tblNumber, 100, 88);
                    NewSimplexsLola = _db.GetSimplexForLola(tblNumber+1, 100, lolaRowindex);

                    double OldLolaValueZ = Math.Abs(_db.LolaValueZ(tblNumber, 100, 88, LolaVarNumCol));
                    smplxZ.Clear();
                    foreach (var OldSimplexZItem in OldSimplexsZ)
                    {
                        double value = OldSimplexZItem.Value;

                        smplxZ.Add(new DomainClasses.Simplex.Simplex()
                        {
                            ColIndex = OldSimplexZItem.ColIndex,
                            InOut = 0,
                            Min = false,
                            Out = false,
                            RowIndex = OldSimplexZItem.RowIndex,
                            StCode = 100,
                            TblNumber = OldSimplexZItem.TblNumber + 1,
                            TypeVar = 0,
                            VarNumCol = OldSimplexZItem.VarNumCol,
                            VarNumRow = OldSimplexZItem.VarNumRow,
                            Value = Convert.ToDouble(string.Format("{0:0.000}", value + (OldLolaValueZ * GetValueOfSimplex(NewSimplexsLola, OldSimplexZItem.ColIndex))))
                        });
                    };
                    _db.InsertSimplex(smplxZ);
                    _db.Save();

                    //End Process Z Row

                    //Begin Process Another Row 
                    int varRowNum = _db.GetVarNumRowOfSimplex(tblNumber, 100);

                    anotherSimplexRow = _db.GetAnotherSimplexRows(tblNumber, 100, varRowNum);
                    anotherSmplxObj.Clear();
                    foreach (var OldSimplexRowItem in anotherSimplexRow)
                    {
                        double value = OldSimplexRowItem.Value;

                        anotherSmplxObj.Add(new DomainClasses.Simplex.Simplex()
                        {
                            ColIndex = OldSimplexRowItem.ColIndex,
                            InOut = 0,
                            Min = false,
                            Out = false,
                            RowIndex = OldSimplexRowItem.RowIndex,
                            StCode = 100,
                            TblNumber = OldSimplexRowItem.TblNumber + 1,
                            TypeVar = 0,
                            VarNumCol = OldSimplexRowItem.VarNumCol,
                            VarNumRow = OldSimplexRowItem.VarNumRow,
                            Value = Convert.ToDouble(string.Format("{0:0.000}", value + (-1 * GetValueOfSimplex(NewSimplexsLola, OldSimplexRowItem.ColIndex))))
                        });
                    };
                    _db.InsertSimplex(anotherSmplxObj);
                    _db.Save();
                    //End Process Another Row
                }
                else
                {
                    ViewBag.EndMessage = "چون سطر Z مثبت شده به جدول بهنیه رسیده ایم ";
                }
                tblNumber = tblNumber + 1;

                //begin Process for Update LolaCol
                List<MyCms.DomainClasses.Simplex.Simplex> UpDownLolaColList = new List<DomainClasses.Simplex.Simplex>();
                UpDownLolaColList = _db.UpDownLolaColList(tblNumber, 100, lolaRowindex, lolaColIndex);
                List<MyCms.DomainClasses.Simplex.Simplex> lolaColObj = new List<MyCms.DomainClasses.Simplex.Simplex>();
                foreach (var LolaColItem  in UpDownLolaColList)
                {
                    double val = 0;
                    if (LolaColItem.RowIndex == lolaRowindex && LolaColItem.ColIndex == lolaColIndex)
                        val = 1;

                    lolaColObj.Add(new DomainClasses.Simplex.Simplex()
                    {
                        ColIndex = LolaColItem.ColIndex,
                        InOut = LolaColItem.InOut,
                        Min = LolaColItem.Min,
                        Out = LolaColItem.Out,
                        RowIndex = LolaColItem.RowIndex,
                        StCode = 100,
                        TblNumber = LolaColItem.TblNumber,
                        TypeVar = 0,
                        VarNumCol = LolaColItem.VarNumCol,
                        VarNumRow = LolaColItem.VarNumRow,
                        Value = val
                    });
                }

                _db.DeleteSimplex(UpDownLolaColList);
                _db.InsertSimplex(lolaColObj);
                _db.Save();

                //End Process for Update LolaCol

                GetSimplexForMin.Clear();
                GetSimplexForMin = _db.GetSimplexForMin(tblNumber, 100);
            } while (GetSimplexForMin.Count > 0);

            return RedirectToAction("Index", "Simplex", new { area = "SimplexPanel" });
            //return View("simplex");
        }

        public double GetValueOfSimplex(List<MyCms.DomainClasses.Simplex.Simplex> NewSimplexsLola  , int colindex)
        {
            foreach (var item in NewSimplexsLola)
            {
                if ( item.ColIndex == colindex)
                    return item.Value;
            }

            return 0; 
        }

        public string GetValue(string NewRow)
        {
            string retValue = NewRow.Substring(0, NewRow.IndexOf('X'));
            if (retValue == "")
                retValue = "1";

            return retValue;
        }
        private bool SimplexExists(int SimplexID)
        {
            return _db.SimplexExists(SimplexID);
        }

        public string AddBaseVariable(Int16 CountRow, string StrValue, int MaxVariable)//string[,] Variable
        {
            string LineStrValue = null;
            bool NewLine = true;
            string RetStrValue = null;
            int n2 = 0;
            string Z = null;
            for (int m2 = 0; m2 < CountRow; m2++)//m
            {
                if (m2 == 0) // Z
                {
                    RetStrValue = ConvertToLinerZ(StrValue, m2);
                }
                else
                {
                    LineStrValue = StrValue.Trim().Split('\n')[m2].Trim();
                    LineStrValue = LineStrValue.Replace("=", "+X" + (MaxVariable + 1).ToString() + "=");
                    MaxVariable = MaxVariable + 1;
                    CountCol = MaxVariable;
                    string BeforSinEquel = null;
                    bool AfterSinEquel = false;
                    string lastSin = null;
                    string NewRow = null;
                    foreach (char c in LineStrValue.ToUpper())
                    {
                        string strChar = c.ToString();
                        if (strChar == "+" || strChar == "-" || strChar == "=")
                        {
                            if (strChar != "=")
                                BeforSinEquel = strChar;

                            n2 = GetVariableIndex(NewRow, false);
                            //Variable[m2, n2] = NewRow;
                            if (strChar == "=")
                            {
                                RetStrValue = (RetStrValue == null) ? NewRow : RetStrValue + BeforSinEquel + NewRow + "=";
                                AfterSinEquel = true;
                            }
                            else if (strChar != "=")
                            {
                                if (RetStrValue != null && NewLine == true)
                                {
                                    RetStrValue = (RetStrValue == null) ? NewRow : RetStrValue + NewRow;
                                    NewLine = false;
                                }
                                else
                                {
                                    RetStrValue = (RetStrValue == null) ? NewRow : RetStrValue + lastSin + NewRow;
                                }
                            }
                            NewRow = "";
                            n2 = n2 + 1;
                            lastSin = strChar;
                        }
                        else
                        {
                            if (AfterSinEquel)
                            {
                                RetStrValue = RetStrValue + strChar ;
                            }
                                

                            NewRow = NewRow + strChar;
                        }

                    }
                    RetStrValue = RetStrValue + "\n";
                    NewLine = true;
                }//end if 
            }// End for
            return RetStrValue;
        }
        public async Task<IActionResult> CheckSimplex(MyCms.DomainClasses.Simplex.Simplex PI_Simplex)
        {
            return View("Simplex");
        }

        #region addSimplex
        private void addSimplex(MyCms.DomainClasses.Simplex.Simplex PI_Simplex)
        {
            _db.InsertSimplex(PI_Simplex);
            _db.Save();
        }

        public void addSimplex(List<MyCms.DomainClasses.Simplex.Simplex> simplices)
        {
            foreach (var item in simplices)
            {
                _db.InsertSimplex(item);
            }
        }

        #endregion

        #region getSimplex
        private IEnumerable<ShowSimplexViewModel> getAllSimplex(int tblNumber, int stCode)
        {
            return _db.GetAllSimplex(tblNumber, stCode);
        }

        private IEnumerable<MyCms.DomainClasses.Simplex.Simplex> getSimplexZ(int tblNumber, int stCode , int varNumRow)
        {
            return _db.GetSimplexForZ(tblNumber, stCode, varNumRow);
        }


        private ShowStViewModel GetSt()
        {
            var ret = _dbSt.GetSt(100);
            if (ret == null)
                return new ShowStViewModel()
                {
                    StValue = "",
                    IsZ = false,
                    StCode = 100,
                    StID = 0,
                    StValueLiner = ""
                };

            return ret;
        }

        #endregion

        #region ConvertToLinerValue
        public string ConvertToLinerValue(Int16 CountRow, string Value)
        {
            string StrValue = null;
            for (int m = 0; m < CountRow; m++)//m
            {
                string Row = Value.Trim().Split('\r')[m].Trim();
                Row = Row.Replace(">", "=").Replace(">=", "=");
                Row = Row.Replace("<", "=").Replace("<=", "=");
                StrValue = StrValue + Row + "\n";
            }
            return StrValue;
        }

        public string ConvertToLinerZ(string StrValue, int m)
        {
            string Z = null;
            string RetStrValue = null;
            Z = StrValue.Trim().Split('\n')[m].Trim();
            bool firstChar = true;
            foreach (char c in Z.ToUpper())
            {
                string strChar = c.ToString();
                if (firstChar)
                {
                    if (strChar == "-")
                        RetStrValue = RetStrValue + "+";

                    if (strChar != "-" && strChar != "+")
                        RetStrValue = RetStrValue + "-" + strChar;

                    firstChar = false;
                }
                else
                {
                    if (strChar == "+")
                    {
                        strChar = "-";
                        RetStrValue = RetStrValue + strChar;
                    }
                    else if (strChar == "-")
                    {
                        strChar = "+";
                        RetStrValue = RetStrValue + strChar;
                    }
                    else
                    {
                        RetStrValue = RetStrValue + strChar;
                    }
                }

            }//end foreach

            RetStrValue = "Z" + RetStrValue + "=0" + "\n";
            return RetStrValue;
        }

        #endregion

        #region GetMaxAndIndexVariable
        public int GetMaxVariable(Int16 CountRow, string Value)
        {
            bool firstChar = true;
            int MaxVariable = 0;
            string StrValue = null;
            for (int m = 0; m < CountRow; m++)//m
            {
                string Row = Value.Trim().Split('\n')[m].Trim();
                StrValue = Row;

                string NewRow = null;
                foreach (char c in StrValue.ToUpper())
                {
                    string strChar = c.ToString();
                    if ((strChar == "+" || strChar == "-" || strChar == "=") && firstChar != true)
                    {
                        int VarIndex = GetVariableIndex(NewRow, true);
                        if (VarIndex > MaxVariable)
                            MaxVariable = VarIndex;
                        NewRow = "";
                    }
                    else
                    {
                        NewRow = NewRow + strChar;
                        firstChar = false;
                    }
                }
            }
            return MaxVariable;
        }

        public int GetVariableIndex(string NewRow, bool ProccessForMax)
        {
            if (ProccessForMax)
                return Convert.ToInt32(NewRow.Substring(NewRow.IndexOf('X') + 1, NewRow.Length - (NewRow.IndexOf('X') + 1)));

            return Convert.ToInt32(NewRow.Substring(NewRow.IndexOf('X') + 1, 1));// Get 2 of 5x2
        }

        #endregion

        #region GetVariableList
        public List<MyCms.DomainClasses.Simplex.Simplex> GetVariableListZ(string StrValue, Int16 CountRow, int MaxVariable, int CountCol)
        {
            List<MyCms.DomainClasses.Simplex.Simplex> simplices = new List<MyCms.DomainClasses.Simplex.Simplex>();
            string[,] Variable = new string[CountRow + 2, MaxVariable + CountRow + 2];
            bool FirstChar = true;
            string lastSin = null;
            int n2 = 0;
            string NewRow = null;
            string Z = StrValue.Trim().Split('\n')[0].Trim();
            foreach (char c in Z.ToUpper())
            {
                string Value = null;
                string strChar = c.ToString();
                if (FirstChar && strChar != "Z")
                {
                    lastSin = strChar;
                    FirstChar = false;
                }
                else if (strChar == "+" || strChar == "-")
                {
                    n2 = Convert.ToInt32(NewRow.Substring(NewRow.IndexOf('X') + 1, 1));// Get 2 of 5x2
                    Value = GetValue(NewRow);
                    if (lastSin == "-")
                        Value = lastSin + Value;

                    Variable[CountRow, n2] = Value;
                    simplices.Add(new DomainClasses.Simplex.Simplex
                    {
                        RowIndex = CountRow + 1,
                        ColIndex = n2,
                        InOut = 0,
                        StCode = 100,
                        TblNumber = 1,
                        TypeVar = 2, // Z
                        Value = Convert.ToInt32(Value),
                        VarNumRow = 88,
                        VarNumCol = n2,
                        Min = false
                    });
                    NewRow = null;
                    lastSin = strChar;
                }
                else if (strChar == "=")
                {
                    n2 = Convert.ToInt32(NewRow.Substring(NewRow.IndexOf('X') + 1, 1));// Get 2 of 5x2
                    Value = GetValue(NewRow); //NewRow.Substring(0, NewRow.IndexOf('X'));// Get value
                    if (lastSin == "-")
                        Value = lastSin + Value;

                    Variable[CountRow, n2] = Value;
                    simplices.Add(new DomainClasses.Simplex.Simplex
                    {
                        RowIndex = CountRow + 1,
                        ColIndex = n2,
                        InOut = 0,
                        StCode = 100,
                        TblNumber = 1,
                        TypeVar = 2, // Z
                        Value = Convert.ToInt32(Value),
                        VarNumRow = 88,
                        VarNumCol = n2,
                        Min = false
                    });
                    NewRow = null;
                    lastSin = strChar;

                    Variable[CountRow, MaxVariable + CountRow] = Z.Trim().Split('=')[1].Trim();
                    simplices.Add(new DomainClasses.Simplex.Simplex
                    {
                        RowIndex = CountRow + 1,
                        ColIndex = MaxVariable + CountRow,
                        InOut = 0,
                        StCode = 100,
                        TblNumber = 1,
                        TypeVar = 2, // Z
                        Value = Convert.ToInt32(Z.Trim().Split('=')[1].Trim()),
                        VarNumRow = 88,
                        VarNumCol = 99,
                        Min = false
                    });
                    NewRow = null;
                    lastSin = strChar;

                    NewRow = null;
                    lastSin = strChar;
                }
                else
                {
                    if (strChar != "Z")
                        NewRow = NewRow + strChar;
                }
            }


            for (int i = 1; i < MaxVariable + CountRow + 1; i++)
            {
                MyCms.DomainClasses.Simplex.Simplex simplex = simplices.Find(r => r.ColIndex == i);
                if (simplex == null)
                {
                    simplices.Add(new DomainClasses.Simplex.Simplex
                    {
                        RowIndex = CountRow + 1,
                        ColIndex = i,
                        InOut = 0,
                        StCode = 100,
                        TblNumber = 1,
                        TypeVar = 0,
                        Value = 0,
                        VarNumRow = 88,
                        VarNumCol = i,
                        Min = false
                    });
                    Variable[CountRow + 1, i] = "0";
                }
            }

            return simplices;
        }
        public List<MyCms.DomainClasses.Simplex.Simplex> GetListVariable(Int16 CountRow, string StrValue, int MaxVariable, int CountCol)
        {
            List<MyCms.DomainClasses.Simplex.Simplex> simplices = new List<MyCms.DomainClasses.Simplex.Simplex>();
            string[,] Variable = new string[CountRow + 2, MaxVariable + CountRow + 2];
            string LineStrValue = null;
            bool NewLine = true;
            string RetStrValue = null;
            int n2 = 0;
            string NewRow = null;
            string lastSin = null;
            string Value = null;

            for (int m2 = 0; m2 < CountRow; m2++)//m
            {
                if (m2 == 0) // Z
                {
                    //Variable =  GetVariableListZ(StrValue , CountRow,MaxVariable,CountCol);
                    simplices = GetVariableListZ(StrValue, CountRow, MaxVariable, CountCol);
                    addSimplex(simplices);
                }
                else
                {
                    NewRow = null;
                    LineStrValue = StrValue.Trim().Split('\n')[m2].Trim();
                    string BeforSinEquel = null;
                    bool AfterSinEquel = false;
                    foreach (char c in LineStrValue.ToUpper())
                    {
                        string strChar = c.ToString();
                        if (strChar == "+" || strChar == "-" || strChar == "=")
                        {
                            if (strChar != "=")
                                BeforSinEquel = strChar;

                            n2 = GetVariableIndex(NewRow, false);
                            Value = GetValue(NewRow);

                            if (strChar == "=")
                            {
                                Variable[m2, n2] = Value;
                                simplices.Add(new DomainClasses.Simplex.Simplex
                                {
                                    RowIndex = m2,
                                    ColIndex = n2,
                                    InOut = 0,
                                    StCode = 100,
                                    TblNumber = 1,
                                    TypeVar = 1,
                                    Value = Convert.ToInt32(Value),
                                    VarNumRow = m2 + NonBaseVarCount,
                                    VarNumCol = n2,
                                    Min = false
                                });

                                Variable[m2, MaxVariable + CountRow] = LineStrValue.Trim().Split('=')[1].Trim();
                                simplices.Add(new DomainClasses.Simplex.Simplex
                                {
                                    RowIndex = m2,
                                    ColIndex = MaxVariable + CountRow,
                                    InOut = 0,
                                    StCode = 100,
                                    TblNumber = 1,
                                    TypeVar = 0,
                                    Value = Convert.ToInt32(LineStrValue.Trim().Split('=')[1].Trim()),
                                    VarNumRow = m2 + NonBaseVarCount,
                                    VarNumCol = 99,
                                    Min = false//MaxVariable + CountRow
                                });
                            }
                            else
                            {
                                if (lastSin == "-")
                                    Value = lastSin + Value;

                                Variable[m2, n2] = Value;
                                 simplices.Add(new DomainClasses.Simplex.Simplex
                                {
                                    RowIndex = m2,
                                    ColIndex = n2,
                                    InOut = 0,
                                    StCode = 100,
                                    TblNumber = 1,
                                    TypeVar = 0,
                                    Value = Convert.ToInt32(Value),
                                    VarNumRow = m2 + NonBaseVarCount,
                                    VarNumCol = n2,//MaxVariable + CountRow
                                     Min = false
                                 });
                            }

                            NewRow = null;
                            n2 = n2 + 1;
                            lastSin = strChar;
                        }
                        else
                        {
                            if (AfterSinEquel)
                                RetStrValue = RetStrValue + strChar + NewRow;

                            NewRow = NewRow + strChar;
                        }
                    }
                    RetStrValue = RetStrValue + "\n";
                    NewLine = true;
                }//end if 
            }// End for

            for (int m = 1; m < CountRow; m++)
            {
                for (int n = 1; n < MaxVariable + CountRow + 1; n++)
                {
                    MyCms.DomainClasses.Simplex.Simplex simplex = simplices.Find(r => r.ColIndex == n && r.RowIndex == m);
                    if (simplex == null)
                    {
                        simplices.Add(new DomainClasses.Simplex.Simplex
                        {
                            RowIndex = m, //CountRow + 1,
                            ColIndex = n,
                            InOut = 0,
                            StCode = 100,
                            TblNumber = 1,
                            TypeVar = 0,
                            Value = 0,
                            VarNumRow = m + NonBaseVarCount,
                            VarNumCol = n,
                            Min = false
                        });
                        Variable[m, n] = "0";
                    }
                }
            }

            addSimplex(simplices);

            return simplices;
        }

        #endregion

        #region DeleteAndDefault
        [HttpPost]
        public IActionResult Delete([Bind("StValue,StCode,IsZ")] St st)
        {
            if (st.IsZ == null)
                st.IsZ = false;

            st.StCode = 100;

            if (ModelState.IsValid)
            {
                _dbSt.DeleteSt(st);
                _dbSt.Save();

                MyCms.DomainClasses.Simplex.Simplex simplex = new MyCms.DomainClasses.Simplex.Simplex()
                {
                    StCode = 100,
                    ColIndex = 0,
                    InOut = 0,
                    RowIndex = 0,
                    SimplexID = 0,
                    TblNumber = 0,
                    TypeVar = 0,
                    Value = 0,
                    VarNumCol = 0,
                    VarNumRow = 0
                };

                _db.DeleteSimplex(simplex);
                _db.Save();
            }

            return RedirectToAction("Index", "Simplex", new { area = "SimplexPanel" });
            //return View("simplex");
        }

        [HttpPost]
        public IActionResult Default([Bind("StValue,StCode,IsZ")] St st)
        {
            ViewBag.StCode = 100;
            ViewBag.StValue = "2x1+3x3\r4x1+5x2+6x3\r5x1+6x2+7x3";
            ViewBag.StValueLiner = "";

            return RedirectToAction("Index", "Simplex", new { area = "SimplexPanel" });
            //return View("simplex");
        }

        #endregion
    }
}
