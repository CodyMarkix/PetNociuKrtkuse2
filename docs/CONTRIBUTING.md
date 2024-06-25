# Pro přispěvatele

Všechno co je potřeba za software je Git a Git LFS přídavek na věci jako textury, audio, atd.

Samozřejmě je též potřeba mít Github účet ;)

## Struktura větví

Větev `master` je, dá se říct, stabilní větev. Kdykoliv přichází změny do `master`, tak jsou předem testované a ověřené že fungují. Do této větve **se nikdy přímo nepushuje!** Místo toho se dává pull request z `development` do `master`.

Větev `development` je kde nastává vývoj hry. Kdykoliv je hotová funkce, ale neověřená, je mergenutá do `development` větve pro ověření a testování.

Kdykoliv chcete přidat novou funkci do PNUK 2, tak stačí udělat fork na Githubu

```bash
git clone -b development <vaše fork>
# Ujistěte se že jste na větvi development, např pomocí `git branch`

# After shit gets done
git commit -m "commit message"
git push
```

Potom se vytvoří pull request na tento repozitář a buď se schválí nebo neschválí. Znova, **nikdy nepůjdou pull requests do `master` větve! Vždy do `development`!**

## Version stringy

Jsou dva druhy version stringů

- v2023.27.10.0001
- v1.0

Dlouhý string reprezentuje nestabilní verzi, naleznuté buď v `development` větvi, nebo v Github actions. Je sestavený z data commitu/postavení vezre, a z IDčka, které se s každou novou verzí ten den sekvenčně zvyšuje.

Krátký string reprezentuje stabilní verzi, naleznutá buď v `master` větvi, nebo v Releases sekci.
