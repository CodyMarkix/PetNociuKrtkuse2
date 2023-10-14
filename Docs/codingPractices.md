# Programovací etiketa

Zde jsou sepsány coding practices specifické k tomuto projektu. **Jakákoliv pull request porušující tyto coding practices bude odmítnuta!** Tyto practices jsou samozřejmě otevřené k diskuzi a můžou se změnit.

## \#1 - Tabulátor vs. Mezery

V projektu jsou **striktně používány 4 mezery na indentaci**. Můžete používat nástroj na automatické konvertování z tabulátoru na mezery, ale tabulátor v zdrojovém kódu nebude.

## \#2 - Složené závorky po deklaracích funkce

Složené závorky (curly braces) po deklaraci funkce bude vždy vedle jména. Nikdy nebude používaná závorka na novém řádku

### Správně ✔️: 
```C#
using UnityEngine;

public class MyComponent : MonoBehaviour {
    void Start() {
        // Yeah uhh maybe some code do beep boop
    }
}
```

### Špatně ❌:
```C#
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    void Start()
    {
        // I wanna commit die
    }
}
```

## \#3 - Pojmenovávání proměnných/funkcí/tříd

**Promněnné** - Používáme camelCase.

```C#
int someInteger = 56;
```

Promněnné co mají napsaný vlastní getter a setter jsou pojmenovány s podtržítkem před jménem a **můžou být modifikované/čtené jen přes jejich funkce!**

```C#
public class ExampleClass {
    int _temperature = 0;

    public int getTemperature() {
        return _temperature;
    }

    public int setTemperature(int newTemp) {
        _temperature = newTemp;
        return _temperature;
    }
}
```


**Funkce** - Používáme PascalCase / UpperCamelCase

```C#
int MyFunction() {
    Debug.Log("Svarto, je ti něco?");
}
```

**Třídy** - Opět používán PascalCase / UpperCamelCase

```C#
class SomeObject {
    string svarta = "spadl";

    string CallSvarta() {
        return "Svarto?!"
    }
}
```

## \#4 - Pojmenovávání assetů v Unity

Velmi by se preferoval PascalCase, ale může se používat i camelCase/Normální pojmenovávání. Pull requests přidávající assety pojmenované s camelCase/Normálním pojmenováváním budou pořád přijaty.
