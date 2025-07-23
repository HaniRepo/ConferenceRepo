using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Conference.DAL
{
   public class Person
    {
        private int _idp;
        public int idp 
        {
            get { return _idp; }
            set { _idp = value; }
        }

        private string _code;
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        private bool _vip;
        public bool vip
        {
            get { return _vip; }
            set { _vip = value; }
        }

        

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _ename;
        public string ename
        {
            get { return _ename; }
            set { _ename = value; }
        }

   


        private string _grade;
        public string grade
        {
            get { return _grade; }
            set { _grade = value; }
        }


        private string _major;
        public string major
        {
            get { return _major; }
            set { _major = value; }
        }

        private string _telephone;
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _mobile;
        public string mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _position;
        public string position
        {
            get { return _position; }
            set { _position = value; }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

    
        private string _pdate;
        public string pdate
        {
            get { return _pdate; }
            set { _pdate = value; }
        }

        private string _ptime;
        public string ptime
        {
            get { return _ptime; }
            set { _ptime = value; }
        }
        public bool DAL_Insert(Person obj)
        {
            return BL.BL_person.BL_Insert(this);
        }


        public DataTable DAL_serach_barcode(string barcode)
        {
            return BL.BL_person.BL_search_barcode(barcode);
        }

        public DataTable DAL_serach_name(string name)
        {
            return BL.BL_person.BL_search_name(name);
        }

        
        public DataTable DAL_selectONE(int idp)
        {
            return BL.BL_person.BL_selectONE(idp);
        }
        //public bool DAL_Update(int idp, string name,  string title)
        //{
            //return BL.BL_person.BL_Update(idp, name, title);
       // }

        public DataTable DAL_SelectALL()
        {
            return BL.BL_person.BL_SelectALL();
        }
    }
}
