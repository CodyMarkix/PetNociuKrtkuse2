# Pro přispěvatele

Všechno co je potřeba za software je Git a Git LFS přídavek na věci jako textury, audio, atd.

## Struktura větví

Větev `master` je, dá se říct, stabilní větev. Kdykoliv přichází změny do `master`, tak jsou předem testované a ověřené že fungují. Do této větve **se nikdy přímo nepushuje!** Místo toho se dává pull request z `development` do `master`.

Větev `development` je kde nastává vývoj hry. Kdykoliv je hotová funkce, ale neověřená, je mergenutá do `development` větve pro ověření a testování.

Kdykoliv chcete přidat novou funkci do PNUK 2, tak si po forknutí projektu vytvoříte novou branch. Něco jako takhle

```bash
git checkout -b your-branch-name-related-to-contents
# After shit gets done
git commit -m "commit message"
git push origin your-branch-name-related-to-contents
```

Potom se vytvoří pull request na tento repozitář a buď se schválí nebo neschválí. Znova, **nikdy nepůjdou pull requests do `master` větve! Vždy do `development`!**