# Souborová strukruta

Tento dokument pokrývá pouze file strukturu v `Assets/` složce. Všechny typy souborů mají jednu složku, i když v té složce bude jenom jeden soubor.

-  `Animations/` - Všechny animace. Složky jsou roztříděné podle objektů nebo skupin objektů (např.: `Animations/Animatronics`). Animation controllers jsou uložené společně s animacema

-  `Audio/` - Všechny zvuky ve hře, rozděleno na hudbu, phone guye a zvukové efekty. Mixér je uložen v `Assets/` složce.

-  `Fonts/` - Zde jsou uloženy ty dva jediné fonty použité ve hře

-  `Icon/` - Need I say more?

-  `Input/` - Zde jsou uloženy Input Action mapy pro globální keybinds a keybinds v kanceláři. Zde nejsou uloženy speciální keybinds, jako přeskočení noci a přepnutí na `Trailer.unity`
  
-  `Materials/` - Zde jsou uloženy materiály pro každý model. Materiály jsou roztříděné do složek podle modelu, ke kterému patří

- `Models/` - Všechny modely jsou uložené zde. Modely **musí být ve formátu .FBX!** Toto má pár výjimek prozatím. V `Models/Blend files` jsou Blender soubory pro vlastně vytvořené modely. V každé `Restaurant` složce jsou uloženy korrespondující soubory pro restauraci (např.: `Blend files/Restaurant` = Blender soubory pro modely restaurace)

- `PostProcessing/` - Post processing profily. V projektu je používán [Post Processing 3.0 balíček od Unity](https://docs.unity3d.com/Packages/com.unity.postprocessing@3.0)

- `Prefabs/` - Prefaby. V samotné hře jsou ale vzácně používané. Tam kde ale jsou tak musí být. Např. UI tlačítka, Game Manager script, atd.

- `Scenes/` - Need I say more?

- `Scripts/` - Všechny skripty napsané pro hru. Jsou roztříděné podle použití ve hře. (Výjimka je `Build/BuildScript.cs`, který je používaný pro Github Actions)

- `Sprites/` - Sprite mapy. Rozdíl mezi `Sprites` a `Textures` je, že ve `Sprites` jsou jenom textury s několika stavy

- `Textures/` - Stejné jako sprites, ale textury s jedním stavem

- `Video/` - Video soubory (zatím využíváno jen pro Game Over scénu)