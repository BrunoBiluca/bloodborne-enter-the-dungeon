using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : EnumX<Tags> {

    public static Tags hunter = new Tags(0, "hunter");
    public static Tags echoes = new Tags(0, "echoes");

    public Tags(int index, string name) : base(index, name) {}

}
