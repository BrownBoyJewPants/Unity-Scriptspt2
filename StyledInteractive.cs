using System;
using UnityEngine;


namespace Boxophobic.StyledGUI
{ 
public class StyledInteractive : PropertyAttribute
{ 
public int value;

public string keyword;

public int type;

public StyledInteractive(int v)
{ 
this.type = 0;
this.value = v;
}

public StyledInteractive(string k)
{ 
this.type = 1;
this.keyword = k;
}
}
}
