using HospitalApiSA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace HospitalApiSA
{
    public static class General
    {
        public static int ShiftNo
        {
            get;
            set;
        }
        
        //public static T GenerateSNo<T>(BazaarEntities db, string table, string column, string where = " where 1=1 ")
        //{
        //    var result = db.Database.SqlQuery<T>("select isnull(max(" + column + "),0) + 1 from " + table + " with(UPDLOCK, TABLOCK)" + where).ToList().FirstOrDefault(); // insert value od date from database engine
        //    return result;
        //}

        //public static T Execute<T>(BazaarEntities db, string commandText)
        //{
        //    var result = db.Database.SqlQuery<T>(commandText).ToList().FirstOrDefault();
        //    return result;
        //}

        public static int ToInt(this string str)
        {
            try
            {
                if (str.IsEmpty())
                {
                    return 0;
                }

                return int.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static long ToLong(this string str)
        {
            try
            {
                if (str.IsEmpty())
                {
                    return 0;
                }

                return long.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ToStr(this object str)
        {
            try
            {
                if (str == null)
                {
                    throw new Exception();
                }

                if (string.IsNullOrEmpty(str.ToString()) || string.IsNullOrWhiteSpace(str.ToString()))
                {
                    return "";
                }

                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ToPaddingString(this object str,int lenght)
        {
            try
            {
                if (str.IsEmpty())
                {
                    return ("").PadLeft(lenght, '0');
                }
                var x = str.ToStr().PadLeft(lenght, '0');
                return x;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsEmpty(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static bool IsEmpty(this object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToStr()) || string.IsNullOrWhiteSpace(obj.ToStr()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static byte[] ConvertToByte(this HttpPostedFile file)
        {
            try
            {
                byte[] imageByte = null;
                BinaryReader rdr = new BinaryReader(file.InputStream);
                imageByte = rdr.ReadBytes((int)file.ContentLength);
                return imageByte;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}