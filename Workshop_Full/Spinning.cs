namespace Workshop
{
    class Spinning
    {
        private double Na_Motor;
        private double Fa_Motor;
        private double D_Motor;
        private double D_Tree;
        private double D_Tool;
        private double Vc_Tool;

        private readonly string racine = "lvc";
        private readonly string element = "Toupie";

        private XmlFile MyXml;

        public Spinning(int vc, int d)
        {
            MyXml = new XmlFile("XmlSettings.xml");
            string Na_Motor_Str = MyXml.Get_Value(racine, element, "V_Motor");
            Na_Motor = double.Parse(Na_Motor_Str);
            string Fa_Motor_Str = MyXml.Get_Value(racine, element, "F_Motor");
            Fa_Motor = double.Parse(Fa_Motor_Str);
            string D_Motor_Str = MyXml.Get_Value(racine, element, "D_Motor");
            D_Motor = double.Parse(D_Motor_Str);
            string D_Tree_Str = MyXml.Get_Value(racine, element, "D_Tree");
            D_Tree = double.Parse(D_Tree_Str);
            Vc_Tool = vc;
            D_Tool = d;
        }

        public double Calcul_Na_Tree()
        {
            double Na_Tree = D_Motor / D_Tree * Na_Motor;

            return Na_Tree;
        }

        public double Calcul_Nr_Tree()
        {
            double Na_Tree = (1000 * 60 * Vc_Tool) / (3.14 * D_Tool);

            return Na_Tree;
        }

        public double Calcul_Ratio()
        {
            double Ratio = Calcul_Na_Tree() / Fa_Motor;

            return Ratio;
        }

        public double Calcul_Fr()
        {
            double Fr = Calcul_Nr_Tree() / Calcul_Ratio();

            return Fr;
        }

        public double Get_Vc_Tool()
        {
            return Vc_Tool;
        }
        
        public double Get_D_Tool()
        {
            return D_Tool;
        }

        public double Get_Na_Motor()
        {
            return Na_Motor;
        }

        public double Get_Fa_Motor()
        {
            return Fa_Motor;
        }

        public double Get_D_Motor()
        {
            return D_Motor;
        }

        public double Get_D_Tree()
        {
            return D_Tree;
        }
    }
}
