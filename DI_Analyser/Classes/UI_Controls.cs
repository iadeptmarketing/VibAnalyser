using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using DI_Analyser.interfaces;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using Analyser.Classes;

namespace DI_Analyser.Classes
{
  public sealed class UI_Controls : UI_Interface
    {
        Form1 objform1 = null;

        public Form1 _Form1
        {
            get
            {
                return objform1;
            }
            set
            {
                objform1 = value;
            }
        }

        public void ChangeColorStyle(int A, int R, int G, int B)
        {
            try
            {
                A = A / 2;
                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(_Form1, DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Black, Color.FromArgb(A, R, G, B));
                Color LightColor = ControlPaint.LightLight(Color.FromArgb(A, R, G, B));
                Color FocusColor = ControlPaint.Light(Color.FromArgb(A, R, G, B));

                _Form1.BackColor = LightColor;
                _Form1.MainTreelist.BackColor = (LightColor);

                if (R == 255 && G == 0 && B == 0)
                {
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.Gold;
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.Gold;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                }
                else
                {
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.Red;
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.Red;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                }
                //_Form1.MainTreelist.Appearance.FocusedRow.BackColor = (FocusColor);
                //_Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = (LightColor);
                //_Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                _Form1.MainTreelist.Appearance.EvenRow.BackColor = (LightColor);
                _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(A, R, G, B);
                _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;

                _Form1._ExpandableSplitter1.BackColor = LightColor;
                _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(A, R, G, B);

                _Form1._ExpandableSplitter2.BackColor = LightColor;
                _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(A, R, G, B);

                _Form1._ExpandableSplitter3.BackColor = LightColor;
                _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(A, R, G, B);

                _Form1._ExpandableSplitter4.BackColor = LightColor;
                _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(A, R, G, B);

                _Form1._ExpandableSplitter5.BackColor = LightColor;
                _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(A, R, G, B);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = (LightColor);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(A, R, G, B);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;


            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }
        public void ChangeColorStyle(string Name)
        {

            try
            {

                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(_Form1, DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.VistaGlass, Color.FromName(Name));
                Color LightColor = ControlPaint.LightLight(Color.FromName(Name));
                Color FocusColor = ControlPaint.Light(Color.FromName(Name));
                _Form1.BackColor = LightColor;
                _Form1.MainTreelist.BackColor = (LightColor);

                if (Name == "Red")
                {
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.Gold;
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.Gold;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;
                }
                else
                {
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.Red;
                    _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.Red;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.Goldenrod;
                    _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;
                }
                //_Form1.MainTreelist.Appearance.FocusedRow.BackColor = (FocusColor);
                //_Form1.MainTreelist.Appearance.FocusedRow.BackColor2 =  (LightColor);
                //_Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                _Form1.MainTreelist.Appearance.EvenRow.BackColor = (LightColor);
                _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromName(Name);
                _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;

                _Form1._ExpandableSplitter1.BackColor = LightColor;
                _Form1._ExpandableSplitter1.BackColor2 = Color.FromName(Name);

                _Form1._ExpandableSplitter2.BackColor = LightColor;
                _Form1._ExpandableSplitter2.BackColor2 = Color.FromName(Name);

                _Form1._ExpandableSplitter3.BackColor = LightColor;
                _Form1._ExpandableSplitter3.BackColor2 = Color.FromName(Name);

                _Form1._ExpandableSplitter4.BackColor = LightColor;
                _Form1._ExpandableSplitter4.BackColor2 = Color.FromName(Name);

                _Form1._ExpandableSplitter5.BackColor = LightColor;
                _Form1._ExpandableSplitter5.BackColor2 = Color.FromName(Name);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.BackColor =  (LightColor);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromName(Name);
                //_Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

        public void ChangeStyle(string StyleName)
        {
            try
            {
                _Form1._panel1.BackColor = Color.FromArgb(247, 245, 241);

                switch (StyleName)
                {
                    case "Caramel":
                        {
                            //tvAlarmList.BackColor = Color.FromArgb(247, 245, 241);
                            //panel1.BackColor = Color.FromArgb(233, 225, 209);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(247, 245, 241);

                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(254, 185, 58);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(233, 225, 209);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(244, 242, 235);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(254, 185, 58);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;


                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(254, 185, 58);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(254, 185, 58);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(254, 185, 58);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(254, 185, 58);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(255, 246, 162);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(254, 185, 58);
                            //msMain.BackColor = Color.FromArgb(233, 225, 209);
                            //tsMain.BackColor = Color.FromArgb(244, 242, 235);
                            // tsBotom1.BackColor = Color.FromArgb(244, 242, 235);
                            break;
                        }
                    case "Money Twins":
                        {
                            //tvAlarmList.BackColor = Color.FromArgb(227, 241, 254);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(227, 241, 254);


                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(174, 216, 255);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(186, 214, 242);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(152, 191, 235);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(174, 216, 255);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;



                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(174, 216, 255);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(174, 216, 255);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(174, 216, 255);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(174, 216, 255);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(207, 238, 255);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(174, 216, 255);
                            
                            //tsMain.BackColor = Color.FromArgb(186, 214, 242);
                            //tsBotom1.BackColor = Color.FromArgb(186, 214, 242);
                            //msMain.BackColor = Color.FromArgb(152, 191, 235);
                            break;
                        }
                    case "Lilian":
                        {
                            //tvAlarmList.BackColor = Color.FromArgb(236, 237, 244);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(236, 237, 244);



                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(247, 242, 247);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(223, 246, 255);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(174, 211, 240);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(247, 242, 247);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;



                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(247, 242, 247);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(247, 242, 247);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(247, 242, 247);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(247, 242, 247);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(224, 203, 223);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(247, 242, 247);
                            
                            //tsMain.BackColor = Color.FromArgb(223, 246, 255);
                            // tsBottom.BackColor = Color.FromArgb(223, 246, 255);
                           // msMain.BackColor = Color.FromArgb(174, 211, 240);
                            break;
                        }
                    case "The Asphalt World":
                        {

                            //tvAlarmList.BackColor = Color.FromArgb(253, 253, 253);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(253, 253, 253);



                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(250, 250, 250);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(206, 221, 252);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.White;
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(250, 250, 250);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;


                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(250, 250, 250);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(250, 250, 250);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(250, 250, 250);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(250, 250, 250);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(225, 225, 225);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(250, 250, 250);
                            // tsBotom1.BackColor = Color.FromArgb(225, 225, 225);
                           // tsMain.BackColor = Color.FromArgb(225, 225, 225);
                            //msMain.BackColor = Color.FromArgb(206, 221, 252);
                            break;
                        }
                    case "iMaginary":
                        {

                            //tvAlarmList.BackColor = Color.FromArgb(240, 240, 240);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(240, 240, 240);



                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(230, 247, 228);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(223, 246, 255);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(174, 211, 240);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(230, 247, 228);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(173, 242, 122);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;



                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(230, 247, 228);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(230, 247, 228);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(230, 247, 228);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(230, 247, 228);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(139, 228, 98);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(230, 247, 228);
                            //tsMain.BackColor = Color.FromArgb(223, 246, 255);
                            // tsBottom.BackColor = Color.FromArgb(223, 246, 255);
                            //msMain.BackColor = Color.FromArgb(174, 211, 240);

                            break;
                        }
                    case "Black":
                        {

                            //tvAlarmList.BackColor = Color.FromArgb(221, 221, 221);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(221, 221, 221);

                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(218, 209, 124);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(190, 190, 190);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(99, 99, 99);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(218, 209, 124);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;



                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(218, 209, 124);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(218, 209, 124);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(218, 209, 124);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(218, 209, 124);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(167, 166, 158);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(218, 209, 124);
                            // tsMain.BackColor = Color.FromArgb(190, 190, 190);
                           // msMain.BackColor = Color.FromArgb(99, 99, 99);
                            //tsBotom1.BackColor = Color.FromArgb(190, 190, 190);
                            break;
                        }
                    case "Blue":
                        {

                            //tvAlarmList.BackColor = Color.FromArgb(222, 236, 253);

                            _Form1.MainTreelist.BackColor = Color.FromArgb(222, 236, 253);



                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1.MainTreelist.Appearance.FocusedRow.BackColor2 = Color.FromArgb(114, 153, 208);
                            _Form1.MainTreelist.Appearance.FocusedRow.ForeColor = Color.Black;

                            _Form1.MainTreelist.Appearance.EvenRow.BackColor = Color.FromArgb(73, 119, 186);
                            _Form1.MainTreelist.Appearance.EvenRow.BackColor2 = Color.FromArgb(151, 178, 220);
                            _Form1.MainTreelist.Appearance.EvenRow.ForeColor = Color.Black;


                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.BackColor2 = Color.FromArgb(114, 153, 208);
                            _Form1.MainTreelist.Appearance.HideSelectionRow.ForeColor = Color.Black;

                            _Form1._Datagridview1.BackgroundColor = _Form1._panel1.BackColor;
                            _Form1._Datagridview2.BackgroundColor = _Form1._panel1.BackColor;



                            _Form1._ExpandableSplitter1.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1._ExpandableSplitter1.BackColor2 = Color.FromArgb(114, 153, 208);

                            _Form1._ExpandableSplitter2.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1._ExpandableSplitter2.BackColor2 = Color.FromArgb(114, 153, 208);

                            _Form1._ExpandableSplitter3.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1._ExpandableSplitter3.BackColor2 = Color.FromArgb(114, 153, 208);

                            _Form1._ExpandableSplitter4.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1._ExpandableSplitter4.BackColor2 = Color.FromArgb(114, 153, 208);

                            _Form1._ExpandableSplitter5.BackColor = Color.FromArgb(198, 238, 253);
                            _Form1._ExpandableSplitter5.BackColor2 = Color.FromArgb(114, 153, 208);
                            
                            //msMain.BackColor = Color.FromArgb(73, 119, 186);
                            // tsBottom.BackColor = Color.FromArgb(151, 178, 220);
                            //tsMain.BackColor = Color.FromArgb(151, 178, 220);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
            }
        }

    }
}
