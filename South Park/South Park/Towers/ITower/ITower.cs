namespace South_Park
{
    interface ITower
    {
        string Type { get; set; }



        float Rotation { get; set; }

        int Experience { get; set; }

        int Level { get; set; }

        float RechargeTime { get; set; }

        bool Fire { get; set; }

        //EnabledCondition EnabledCondition { get; set; }

    }
}
