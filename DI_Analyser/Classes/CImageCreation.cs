// disabling Hasp Check  Should be removed **************

using System;
using System.Collections.Generic;
using System.Text;
using Aladdin.HASP;
using Aladdin.Hasp;
using DI_Analyser.interfaces;
using Analyser.Classes;

namespace DI_Analyser.Classes
{
    public sealed class CImageCreation 
    {
        //Hasp m_objHasp = null;
        //Hasp m_objHasp1 = null;
        //HaspFeature m_objHaspFeature = HaspFeature.FromFeature(10);
        //HaspFeature m_objHaspFeature1 = HaspFeature.FromFeature(1);
        //private bool m_bAcknowledgement = false;
        //private int m_iAuth1 = 6830;
        //private int m_iAuth2 = 8503;
        //private int m_iSC = 0;
        //private int m_iPt = 0;
        //private const string SCONSTTEST = "595963602115624431633213621186596218641133591530639169591499595962631564602109613131574176603782572214613166635111597210645185642796632111643136566248588115621845571184624109599664639145629506579955603195609315642171596146605495613181574189631255597172628119571990620579643111604165628776608674574190599187636456577197570116612112629166601417571295601188605709599152593163633421602103571119580455590468590901593100571495593146633164593199600136604184596136571175577125597130594127572985639681629117621206628941631998641681598107609121566149590167634116601761620110633374633768640130613159611194573169593651599141566192575624570871644225637607608606605146608990572561633206605165604605623187571145636459633502597911638193576192627154597183624851574523621298570883596889589990635107628982599159577327596580589624574172580291572549589119599716597390637742574175609225631173640175621100590155628138572106633309613798641687591175604192637125633172592107633101623572596148607186579176594386571801608111613173597173634212639150589116603205633145603152627195574184578193636160600126591912645144641727605165631206588770598184605152625722641804599151609214604136636448579623622137611672588458640136579217612617578221609141577175572123575205630399613149597693578194639756608147622119588195602133592127588761570210629184566176593197627174589101596928571614580155634194580160579830640148597127622138633561612138604646571890598259611552575423626136609370591840642829645920637728594397612202642155604125594149573612608461643763595175580393623102598115576489571170597107607112600938577661612545600671637833573912589877635131630445566125609209599193627182601139598113577911605927632199590574644281595386608349578148639168603816580625644955609995638177638188600168641991642149636204623158579178573284636249638204620127596114624246598814578244642403603673620108571360600141592112571106580290574113570794644187632589633132580100622200605208641210571203613168573711600171622136636440628192628196603951636627603199579304589144594128605139643168596123574127622142580168643595604173607165592818579139578200643699611175632990639100605142576395637183593131598126606706590102630678624118580233622130579547626458571163597122622213642183631165605173588105596869610187570185620666642150574297621293639204612320570150574188613175621969643209637135636159643618570129570228573158629157570560592944641205573386595486576127566258640174631140596170611118632589588208590914575169571683595116590195591963606677608209591358626182625491640169630172575507577279592177606151595170594163628152611162598167571178621194624279603145602117566347588295613105590133642699612116570103602118577180571911571159596319606120636903588136637201635417574156600834632470610765580189601212627204628612609170571437602462580138639205624882627888632497590193621673598206601144589485633194633538642692600147623618644861595206590138631428601139606252608147580136642383579159637988591406639256608322632116621726590114601723604570621194645189612290635709641196576141571230633145645902574207634177643295580192580144602127624125577212624137590127608118611209592206608690606498622144636165626153633416607173599280633190566795573196642121638167608865570832620135626156636116640116627944644569597143574758608129622121597111641112574121621118588149594201589168612360592202620120599167623141625313579202578196645186603621605172630977636481638180574191593409606426620770644195594112594229613141622149636140576188641144598445636421632143642528606123571408629923632279608190566179623180607130639208644124632127643135589122578209607126633192609816570155613134595718624784633418595168642190643165576109572537645195600193637127644201636160578525622133634152589191630539591201621204580123636752566127592831626709626981604817636183639154601143625210637147642103641122573716592189633821613868629116629767572813602125642200591151612816577138627181599827641204622566577149576187641614644165629122588211577166626173595977570925613841635911639114611180612197589194642975580383631195637159622708637104600103591158572123624101636900604190576147579147632131607213592288608977604794613231633861599152578456599750611230633123588203644239624207645204611112592192576167640102588406634143574101621451635200645453571837644118601427632202632121605189590172637164597176627850591622603163577101579174571108620159637379604120588136642162645115604797603165574132599904622707593230632114571106607202637111626198629112608341636154634182630110621194608168609479606166604388566625611122636142633209622132572207621208635960575105611196642123605382604144629405634167570124570144642505640166609579613580632188643187566785623119594672573207604140592491643577633166612880571210566764609171642433643204604954573152603300611119642109639969630130643745609737607104624150633155643200591502613117606957580108573195579887610149580640620906608653606186637688595468571477607291624358577421604683622201593123630134641491634112590136588152597471574916612571603398626453621177604854605124609636626746634488573207631102580956623143576108570140611487608874574578641510630178606491602101588387605316620357573163641106566658590118622553580173593170613148600211634920590600620453644102639101603159643197623402602195578435641154599187571584606149604208635442609249639790607101638830599424638170574179627136632142572137627180631212588628603433633333575147575161635145609168630808566174629200626171600106593195613884612121638209620922579630576104589914596723595171627195638947566682628653588169629136598948609316606174594179612141642200590340574151601101570665627254600127624212609127640846598396625122622209604102622336642490622184623136575791607323577141604140584493";
        //public static string Vcode = null;
       
        //public CImageCreation()
        //{
        //    m_objHasp = new Hasp(m_objHaspFeature);
        //    m_objHasp1 = new Hasp(m_objHaspFeature1);

        //}//end(public ImageCreation())
        //public string decrypt(string s)
        //{
        //    try
        //    {
        //        string r = "";

        //        for (int c = 0; c < s.Length; c += 6)
        //        {
        //            int no = Convert.ToInt32(s.Substring(c, 3)) - 523;
        //            byte curchar = Convert.ToByte(no.ToString());
        //            r += (char)curchar;
        //        }
        //        Vcode = r;
        //        return r;
        //    }
        //    catch (Exception mex)
        //    {
        //        return "";
        //    }

        //}
        //public void FirstTest(byte[] Param1)
        //{
        //    try
        //    {
        //        if (m_objHasp != null)
        //            m_objHasp.Encrypt(Param1);
        //        if (m_objHasp1 != null)
        //            m_objHasp1.Encrypt(Param1);


        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }//end(catch (Exception ex))
        //}

        //public void FirstTest(double[] Param1)
        //{

        //    try
        //    {
        //        if (m_objHasp != null && m_objHasp1 != null)
        //        {
        //            HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
        //            HaspStatus objStatus1 = m_objHasp1.Login(decrypt(SCONSTTEST));
        //            if (HaspStatus.AlreadyLoggedIn == objStatus)
        //                m_objHasp.Logout();
        //            if (HaspStatus.StatusOk != objStatus && HaspStatus.AlreadyLoggedIn == objStatus)
        //                m_objHasp.Login(decrypt(SCONSTTEST));
        //            if (HaspStatus.AlreadyLoggedIn == objStatus1)
        //                m_objHasp1.Logout();
        //            if (HaspStatus.StatusOk != objStatus && HaspStatus.AlreadyLoggedIn == objStatus1)
        //                m_objHasp1.Login(decrypt(SCONSTTEST));

        //            m_objHasp.Encrypt(Param1);
        //        }
        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }//end(catch (Exception ex))
        //}

        //public bool Test(string sText)
        //{
        //    bool bTest = false;
        //    try
        //    {
        //        if (sText == "Second")
        //        {
        //            bTest = NewTest();
        //        }//end(if (sText == "Second"))
        //        return bTest;
        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        return bTest;
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }
        //}


        //public bool NewTest()
        //{
        //    int iRlt = 0;
        //    int iSts = 0;
        //    bool bRetRes = false;

        //    try
        //    {
        //        object oConvRlt = (object)iRlt;
        //        object oConvSts = (object)iSts;

        //        HaspKey.Hasp(HaspService.IsHasp4, m_iSC, m_iPt, m_iAuth1, m_iAuth2, oConvRlt, null, oConvSts, null);


        //        iRlt = (int)oConvRlt;

        //        if (iRlt == 1)
        //        {


        //            bRetRes = true;
        //        }
        //        else if (iRlt != 1)
        //        {
        //            bRetRes = false;
        //        }

        //        return bRetRes;

        //    }
        //    catch (Exception ex)
        //    {
        //        return bRetRes;

        //    }
        //}

        //public bool GetAndCheck()
        //{
        //    bool bCheckID = false;
        //    int idLw = 0;
        //    int idHgh = 0;
        //    int iSts = 0;

        //    try
        //    {

        //        object oConvidLw = (object)idLw;
        //        object oConvidHigh = (object)idHgh;
        //        object oConvSts = (object)iSts;

        //        HaspKey.Hasp(HaspService.HaspID,
        //        m_iSC,
        //        m_iPt,
        //        m_iAuth1,
        //        m_iAuth2,
        //        oConvidLw,
        //        oConvidHigh,
        //        oConvSts,
        //        null);

        //        idLw = (int)oConvidLw;
        //        idHgh = (int)oConvidHigh;

        //        int iNewOne = idHgh + idLw;

        //        if (iNewOne == 22545)
        //            bCheckID = true;
        //        else
        //            bCheckID = false;

        //        return bCheckID;

        //    }
        //    catch (Exception ex)
        //    {
        //        return bCheckID;

        //    }
        //}

        //public bool Check(byte[] Param1)
        //{
        //    int iFirst = 0;
        //    int iSecond = 0;
        //    int iThird = 0;
        //    bool bEnc = false;

        //    try
        //    {
        //        iSecond = Param1.Length;

        //        object objSecConv = (object)iSecond;
        //        object objThirdConv = (object)iThird;

        //        HaspKey.Hasp(HaspService.EncodeData, m_iSC, m_iPt, m_iAuth1, m_iAuth2, null, objSecConv, objThirdConv, Param1);

        //        iThird = (int)objThirdConv;
        //        if (iThird == 0)
        //        {
        //            bEnc = true;
        //        }
        //        else
        //            bEnc = false;

        //        return bEnc;
        //    }
        //    catch (Exception ex)
        //    {
        //        return bEnc;

        //    }
        //}

        //public bool UnCheck(byte[] Param2)
        //{
        //    int iFirst = 0;
        //    int iSecond = 0;
        //    int iThird = 0;
        //    bool bEnc = false;

        //    try
        //    {
        //        iSecond = Param2.Length;

        //        object objSecConv = (object)iSecond;
        //        object objThirdConv = (object)iThird;

        //        HaspKey.Hasp(HaspService.DecodeData, m_iSC, m_iPt, m_iAuth1, m_iAuth2, null, objSecConv, objThirdConv, Param2);


        //        iThird = (int)objThirdConv;
        //        if (iThird == 0)
        //        {
        //            bEnc = true;
        //        }
        //        else
        //            bEnc = false;

        //        return bEnc;
        //    }
        //    catch (Exception ex)
        //    {
        //        return bEnc;

        //    }
        //}

        //public bool SecondTest(byte[] Param2)
        //{

        //    try
        //    {
        //        if (m_objHasp != null && m_objHasp1 != null)
        //        {
        //            m_objHasp.Logout();
        //            m_objHasp1.Logout();
        //            HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
        //            HaspStatus objStatus1 = m_objHasp1.Login(decrypt(SCONSTTEST));
        //            if (HaspStatus.StatusOk == objStatus || HaspStatus.StatusOk == objStatus1 || HaspStatus.AlreadyLoggedIn == objStatus || HaspStatus.AlreadyLoggedIn == objStatus1)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //            return false;

        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        return m_bAcknowledgement;
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }//end(catch (Exception ex))
        //}

        //public bool SecondTest(double[] Param2)
        //{

        //    try
        //    {
        //        if (m_objHasp != null)
        //        {
        //            HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
        //            HaspStatus objDecryption = m_objHasp.Decrypt(Param2);

        //            if (HaspStatus.StatusOk == objStatus && HaspStatus.StatusOk == objDecryption)
        //            {
        //                m_bAcknowledgement = true;
        //            }
        //        }

        //        return m_bAcknowledgement;

        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        return m_bAcknowledgement;
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }//end(catch (Exception ex))
        //}

        //public byte[] GetBytes()
        //{
        //    string sConvertedGuid = null;
        //    byte[] arrBytesToReturn = null;

        //    try
        //    {
        //        Guid objGuid = Guid.NewGuid();

        //        if (objGuid != null)
        //        {
        //            sConvertedGuid = objGuid.ToString();
        //            arrBytesToReturn = Encoding.ASCII.GetBytes(sConvertedGuid);
        //        }//end(if (objGuid != null))
        //        return arrBytesToReturn;

        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        return arrBytesToReturn;
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }
        //}

        //public double[] GetDouble()
        //{
        //    string sConvertedGuid = null;
        //    double[] arrBytesToReturn = null;
        //    char[] carrCharacters = null;

        //    try
        //    {
        //        Guid objGuid = Guid.NewGuid();

        //        if (objGuid != null)
        //        {
        //            sConvertedGuid = objGuid.ToString();
        //            carrCharacters = sConvertedGuid.ToCharArray();
        //            arrBytesToReturn = new double[carrCharacters.Length];
        //            for (int iCtr = 0; iCtr < carrCharacters.Length - 1; iCtr++)
        //            {

        //                arrBytesToReturn[iCtr] = Convert.ToDouble(carrCharacters[iCtr]);
        //            }
        //        }//end(if (objGuid != null))
        //        return arrBytesToReturn;

        //    }//end(try)
        //    catch (Exception ex)
        //    {
        //        return arrBytesToReturn;
        //        System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
        //    }
        //}
    
    
    
        Hasp m_objHasp = null;
        Hasp m_objHasp1 = null;
        HaspFeature m_objHaspFeature = HaspFeature.FromFeature(10);
        HaspFeature m_objHaspFeature1 = HaspFeature.FromFeature(1);
        private bool m_bAcknowledgement = false;
        private int m_iAuth1 = 6830;
        private int m_iAuth2 = 8503;
        private int m_iSC = 0;
        private int m_iPt = 0;


        private const string SCONSTTEST = "595963602115624431633213621186596218641133591530639169591499595962631564602109613131574176603782572214613166635111597210645185642796632111643136566248588115621845571184624109599664639145629506579955603195609315642171596146605495613181574189631255597172628119571990620579643111604165628776608674574190599187636456577197570116612112629166601417571295601188605709599152593163633421602103571119580455590468590901593100571495593146633164593199600136604184596136571175577125597130594127572985639681629117621206628941631998641681598107609121566149590167634116601761620110633374633768640130613159611194573169593651599141566192575624570871644225637607608606605146608990572561633206605165604605623187571145636459633502597911638193576192627154597183624851574523621298570883596889589990635107628982599159577327596580589624574172580291572549589119599716597390637742574175609225631173640175621100590155628138572106633309613798641687591175604192637125633172592107633101623572596148607186579176594386571801608111613173597173634212639150589116603205633145603152627195574184578193636160600126591912645144641727605165631206588770598184605152625722641804599151609214604136636448579623622137611672588458640136579217612617578221609141577175572123575205630399613149597693578194639756608147622119588195602133592127588761570210629184566176593197627174589101596928571614580155634194580160579830640148597127622138633561612138604646571890598259611552575423626136609370591840642829645920637728594397612202642155604125594149573612608461643763595175580393623102598115576489571170597107607112600938577661612545600671637833573912589877635131630445566125609209599193627182601139598113577911605927632199590574644281595386608349578148639168603816580625644955609995638177638188600168641991642149636204623158579178573284636249638204620127596114624246598814578244642403603673620108571360600141592112571106580290574113570794644187632589633132580100622200605208641210571203613168573711600171622136636440628192628196603951636627603199579304589144594128605139643168596123574127622142580168643595604173607165592818579139578200643699611175632990639100605142576395637183593131598126606706590102630678624118580233622130579547626458571163597122622213642183631165605173588105596869610187570185620666642150574297621293639204612320570150574188613175621969643209637135636159643618570129570228573158629157570560592944641205573386595486576127566258640174631140596170611118632589588208590914575169571683595116590195591963606677608209591358626182625491640169630172575507577279592177606151595170594163628152611162598167571178621194624279603145602117566347588295613105590133642699612116570103602118577180571911571159596319606120636903588136637201635417574156600834632470610765580189601212627204628612609170571437602462580138639205624882627888632497590193621673598206601144589485633194633538642692600147623618644861595206590138631428601139606252608147580136642383579159637988591406639256608322632116621726590114601723604570621194645189612290635709641196576141571230633145645902574207634177643295580192580144602127624125577212624137590127608118611209592206608690606498622144636165626153633416607173599280633190566795573196642121638167608865570832620135626156636116640116627944644569597143574758608129622121597111641112574121621118588149594201589168612360592202620120599167623141625313579202578196645186603621605172630977636481638180574191593409606426620770644195594112594229613141622149636140576188641144598445636421632143642528606123571408629923632279608190566179623180607130639208644124632127643135589122578209607126633192609816570155613134595718624784633418595168642190643165576109572537645195600193637127644201636160578525622133634152589191630539591201621204580123636752566127592831626709626981604817636183639154601143625210637147642103641122573716592189633821613868629116629767572813602125642200591151612816577138627181599827641204622566577149576187641614644165629122588211577166626173595977570925613841635911639114611180612197589194642975580383631195637159622708637104600103591158572123624101636900604190576147579147632131607213592288608977604794613231633861599152578456599750611230633123588203644239624207645204611112592192576167640102588406634143574101621451635200645453571837644118601427632202632121605189590172637164597176627850591622603163577101579174571108620159637379604120588136642162645115604797603165574132599904622707593230632114571106607202637111626198629112608341636154634182630110621194608168609479606166604388566625611122636142633209622132572207621208635960575105611196642123605382604144629405634167570124570144642505640166609579613580632188643187566785623119594672573207604140592491643577633166612880571210566764609171642433643204604954573152603300611119642109639969630130643745609737607104624150633155643200591502613117606957580108573195579887610149580640620906608653606186637688595468571477607291624358577421604683622201593123630134641491634112590136588152597471574916612571603398626453621177604854605124609636626746634488573207631102580956623143576108570140611487608874574578641510630178606491602101588387605316620357573163641106566658590118622553580173593170613148600211634920590600620453644102639101603159643197623402602195578435641154599187571584606149604208635442609249639790607101638830599424638170574179627136632142572137627180631212588628603433633333575147575161635145609168630808566174629200626171600106593195613884612121638209620922579630576104589914596723595171627195638947566682628653588169629136598948609316606174594179612141642200590340574151601101570665627254600127624212609127640846598396625122622209604102622336642490622184623136575791607323577141604140584493";
        public static string Vcode = null;
        //private const string SCONSTTEST = "595172602172624172633172621172596172641172591172639172591172595172631172602172613172574172603172572172613172635172597172645172642172632172643172566172588172621172571172624172599172639172629172579172603172609172642172596172605172613172574172631172597172628172571172620172643172604172628172608172574172599172636172577172570172612172629172601172571172601172605172599172593172633172602172571172580172590172590172593172571172593172633172593172600172604172596172571172577172597172594172572172639172629172621172628172631172641172598172609172566172590172634172601172620172633172633172640172613172611172573172593172599172566172575172570172644172637172608172605172608172572172633172605172604172623172571172636172633172597172638172576172627172597172624172574172621172570172596172589172635172628172599172577172596172589172574172580172572172589172599172597172637172574172609172631172640172621172590172628172572172633172613172641172591172604172637172633172592172633172623172596172607172579172594172571172608172613172597172634172639172589172603172633172603172627172574172578172636172600172591172645172641172605172631172588172598172605172625172641172599172609172604172636172579172622172611172588172640172579172612172578172609172577172572172575172630172613172597172578172639172608172622172588172602172592172588172570172629172566172593172627172589172596172571172580172634172580172579172640172597172622172633172612172604172571172598172611172575172626172609172591172642172645172637172594172612172642172604172594172573172608172643172595172580172623172598172576172571172597172607172600172577172612172600172637172573172589172635172630172566172609172599172627172601172598172577172605172632172590172644172595172608172578172639172603172580172644172609172638172638172600172641172642172636172623172579172573172636172638172620172596172624172598172578172642172603172620172571172600172592172571172580172574172570172644172632172633172580172622172605172641172571172613172573172600172622172636172628172628172603172636172603172579172589172594172605172643172596172574172622172580172643172604172607172592172579172578172643172611172632172639172605172576172637172593172598172606172590172630172624172580172622172579172626172571172597172622172642172631172605172588172596172610172570172620172642172574172621172639172612172570172574172613172621172643172637172636172643172570172570172573172629172570172592172641172573172595172576172566172640172631172596172611172632172588172590172575172571172595172590172591172606172608172591172626172625172640172630172575172577172592172606172595172594172628172611172598172571172621172624172603172602172566172588172613196590196642196612196570196602196577196571196571196596196606196636196588196637196635196574196600196632196610196580196601196627196628196609196571196602196580196639196624196627196632196590196621196598196601196589196633196633196642196600196623196644196595196590196631196601196606196608196580196642196579196637196591196639196608196632196621196590196601196604196621196645196612196635196641196576196571196633196645196574196634196643196580196580196602196624196577196624196590196608196611196592196608196606196622196636196626196633196607196599196633196566196573196642196638196608196570196620196626196636196640196627196644196597196574196608196622196597196641196574196621196588196594196589196612196592196620196599196623196625196579196578196645196603196605196630196636196638196574196593196606196620196644196594196594196613196622196636196576196641196598196636196632196642196606196571196629196632196608196566196623196607196639196644196632196643196589196578196607196633196609196570196613196595196624196633196595196642196643196576196572196645196600196637196644196636196578196622196634196589196630196591196621196580196636196566196592196626196626196604196636196639196601196625196637196642196641196573196592196633196613196629196629196572196602196642196591196612196577196627196599196641196622196577196576196641196644196629196588196577196626196595196570196613196635196639196611196612196589196642196580196631196637196622196637196600196591196572196624196636196604196576196579196632196607196592196608196604196613196633196599196578196599196611196633196588196644196624196645196611196592196576196640196588196634196574196621196635196645196571196644196601196632196632196605196590196637196597196627196591196603196577196579196571196620196637196604196588196642196645196604196603196574196599196622196593196632196571196607196637196626196629196608196636196634196630196621196608196609196606196604196566196611196636196633196622196572196621196635196575196611196642196605196604196629196634196570196570196642196640196609196613196632196643196566196623196594196573196604196592196643196633196612196571196566196609196642196643196604196573196603196611196642196639196630196643196609196607196624196633196643196591196613196606196580196573196579196610196580196620196608196606196637196595196571196607196624196577196604196622196593196630196641196634196590196588196597196574196612196603196626196621196604196605196609196626196634196573196631196580196623196576196570196611196608196574196641196630196606196602196588196605196620196573196641196566196590196622196580196593196613196600196634196590196620196644196639196603196643196623196602196578196641196599196571196606196604196635196609196639196607196638196599196638196574196627196632196572196627196631196588196603196633196575196575196635196609196630196566196629196626196600196593196613196612196638196620196579196576196589196596196595196627196638196566196628196588196629196598196609196606196594196612196642196590196574196601196570196627196600196624196609196640196598196625196622196604196622196642196622196623196575196607196577196604196584196";
    

        public CImageCreation()
        {
            m_objHasp = new Hasp(m_objHaspFeature);
            m_objHasp1 = new Hasp(m_objHaspFeature1);

        }//end(public ImageCreation())
        public  string decrypt(string s)
        {
            try
            {
                string r = "";

                for (int c = 0; c < s.Length; c += 6)
                {
                    int no = Convert.ToInt32(s.Substring(c, 3)) - 523;
                    byte curchar = Convert.ToByte(no.ToString());
                    r += (char)curchar;
                }
                Vcode = r;
                return r;
            }
            catch (Exception mex)
            {
                ErrorLog_Class.ErrorLogEntry(mex);
                return "";
            }

        }
        public void FirstTest(byte[] Param1)
        {
            try
            {
                if (m_objHasp != null)
                    m_objHasp.Encrypt(Param1);
                if(m_objHasp1!=null)
                    m_objHasp1.Encrypt(Param1);
 

            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }

        public void FirstTest(double[] Param1)
        {
             
            try
            {
                if (m_objHasp != null && m_objHasp1!=null)
                {
                    HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
                    HaspStatus objStatus1 = m_objHasp1.Login(decrypt(SCONSTTEST));
                    if (HaspStatus.AlreadyLoggedIn == objStatus)
                        m_objHasp.Logout();
                    if(HaspStatus.StatusOk!=objStatus && HaspStatus.AlreadyLoggedIn == objStatus)
                        m_objHasp.Login(decrypt(SCONSTTEST));
                    if (HaspStatus.AlreadyLoggedIn == objStatus1)
                        m_objHasp1.Logout();
                    if (HaspStatus.StatusOk != objStatus && HaspStatus.AlreadyLoggedIn == objStatus1)
                        m_objHasp1.Login(decrypt(SCONSTTEST));
                    
                    m_objHasp.Encrypt(Param1);
                }
            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }

        public bool Test(string sText)
        {
            bool bTest = false;
            try
            {
                if (sText == "Second")
                {
                    bTest = NewTest();
                }//end(if (sText == "Second"))
                return bTest;
            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return bTest;
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        
            private bool NewTest()
        {
            int iRlt = 0;
            int iSts = 0;
            bool bRetRes = false;

            try
            {
                object oConvRlt = (object)iRlt;
                object oConvSts = (object)iSts;

                HaspKey.Hasp(HaspService.IsHasp4, m_iSC, m_iPt, m_iAuth1, m_iAuth2, oConvRlt, null, oConvSts, null);
                

                iRlt = (int)oConvRlt;

                if (iRlt == 1 )
                {

                    
                    bRetRes = true;
                }
                else if(iRlt!=1)
                {
                    bRetRes = false;
                }

                return bRetRes;
                
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return bRetRes;
                
            }
        }

        private bool GetAndCheck()
        {
            bool bCheckID = false;
            int idLw = 0;
            int idHgh = 0;
            int iSts = 0;

            try
            {

                object oConvidLw = (object)idLw;
                object oConvidHigh = (object)idHgh;
                object oConvSts = (object)iSts;

                HaspKey.Hasp(HaspService.HaspID,
                m_iSC,
                m_iPt,
                m_iAuth1,
                m_iAuth2,
                oConvidLw,
                oConvidHigh,
                oConvSts,
                null);

                idLw = (int)oConvidLw;
                idHgh = (int)oConvidHigh;

                int iNewOne = idHgh + idLw;

                if (iNewOne == 22545)
                    bCheckID = true;
                else
                    bCheckID = false;

                return bCheckID;

            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return bCheckID;

            }
        }

        public bool Check(byte[] Param1)
        {
            int iFirst = 0;
            int iSecond = 0;
            int iThird = 0;
            bool bEnc = false;

            try
            {
                iSecond = Param1.Length;

                object objSecConv = (object)iSecond;
                object objThirdConv = (object)iThird;

                HaspKey.Hasp(HaspService.EncodeData, m_iSC, m_iPt, m_iAuth1, m_iAuth2, null, objSecConv, objThirdConv, Param1);

                iThird = (int)objThirdConv;
                if (iThird == 0)
                {
                    bEnc = true;
                }
                else
                    bEnc = false;

                return bEnc;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return bEnc;

            }
        }

        public bool UnCheck(byte[] Param2)
        {
            int iFirst = 0;
            int iSecond = 0;
            int iThird = 0;
            bool bEnc = false;

            try
            {
                iSecond = Param2.Length;

                object objSecConv = (object)iSecond;
                object objThirdConv = (object)iThird;

                HaspKey.Hasp(HaspService.DecodeData, m_iSC, m_iPt, m_iAuth1, m_iAuth2, null, objSecConv, objThirdConv, Param2);


                iThird = (int)objThirdConv;
                if (iThird == 0)
                {
                    bEnc = true;
                }
                else
                    bEnc = false;

                return bEnc;
            }
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return bEnc;

            }
        }

        public bool SecondTest(byte[] Param2)
        {
            
            try
            {
                if (m_objHasp != null && m_objHasp1 != null)
                {
                    m_objHasp.Logout();
                    m_objHasp1.Logout();
                    HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
                    HaspStatus objStatus1 = m_objHasp1.Login(decrypt(SCONSTTEST));
                    //return true;  //  disabling Hasp Check  Should be removed **************
                    if (HaspStatus.StatusOk == objStatus || HaspStatus.StatusOk == objStatus1 || HaspStatus.AlreadyLoggedIn == objStatus || HaspStatus.AlreadyLoggedIn == objStatus1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;

            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return m_bAcknowledgement;
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }

        public bool SecondTest(double[] Param2)
        {

            try
            {
                if (m_objHasp != null)
                {
                    HaspStatus objStatus = m_objHasp.Login(decrypt(SCONSTTEST));
                    HaspStatus objDecryption = m_objHasp.Decrypt(Param2);

                    if (HaspStatus.StatusOk == objStatus && HaspStatus.StatusOk == objDecryption)
                    {
                        m_bAcknowledgement = true;
                    }
                }

                return m_bAcknowledgement;

            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return m_bAcknowledgement;
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }//end(catch (Exception ex))
        }

        public byte[] GetBytes()
        {
            string sConvertedGuid = null;
            byte[] arrBytesToReturn=null;

            try
            {
                Guid objGuid = Guid.NewGuid();

                if (objGuid != null)
                {
                    sConvertedGuid = objGuid.ToString();
                    arrBytesToReturn=Encoding.ASCII.GetBytes(sConvertedGuid);
                }//end(if (objGuid != null))
                return arrBytesToReturn;

            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return arrBytesToReturn;
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        public double[] GetDouble()
        {
            string sConvertedGuid = null;
            double[] arrBytesToReturn = null;
            char[] carrCharacters = null;

            try
            {
                Guid objGuid = Guid.NewGuid();

                if (objGuid != null)
                {
                    sConvertedGuid = objGuid.ToString();
                    carrCharacters=sConvertedGuid.ToCharArray();
                    arrBytesToReturn=new double[carrCharacters.Length];
                    for (int iCtr = 0; iCtr < carrCharacters.Length - 1; iCtr++)
                    {
                        
                        arrBytesToReturn[iCtr]=Convert.ToDouble(carrCharacters[iCtr]);
                    }
                }//end(if (objGuid != null))
                return arrBytesToReturn;

            }//end(try)
            catch (Exception ex)
            {
                ErrorLog_Class.ErrorLogEntry(ex);
                return arrBytesToReturn;
                System.Diagnostics.Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }


    
    
    }


}
