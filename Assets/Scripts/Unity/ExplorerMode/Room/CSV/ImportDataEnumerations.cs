using System.ComponentModel;

public class ImportDataEnumerations {
    public enum Raumart {

        [Description ("A1 Pat/Dienst Stat.,Amb+Funk")]
        A1, [Description ("A2 Pat/Dienst Stat.,Spez.Hygie")]
        A2, [Description ("A3 Pat/Dienst Stat.,Erhöht.Auf")]
        A3, [Description ("A4 Teilküchen (Stationsküchen)")]
        A4, [Description ("A5 Speiseräume")]
        A5, [Description ("A6 Nahrungszubereit./Großküche")]
        A6, [Description ("B1 Büro Verw. u. ärztl. Sektor")]
        B1, [Description ("B2 Wohnräume")]
        B2, [Description ("C1 OP-Räume,Geburtshilfen")]
        C1, [Description ("C2 Endosk,Steril,Schleus,Reini")]
        C2, [Description ("D1 Labore,Blut/Organb.,Leichen")]
        D1, [Description ("E1 Therapie,Elek/Bewegungsther")]
        E1, [Description ("F1 Sanitär/Baderäume")]
        F1, [Description ("F2 Massage/Bäderabteilung")]
        F2, [Description ("F3 Wäscherei,Bettenzentrale")]
        F3, [Description ("G1 Flure,Einghallen,Wartezonen")]
        G1, [Description ("H1 Treppen,Aufzugs/Förderanl")]
        H1, [Description ("I1 Pausenräume,Bereitschaftsz.")]
        I1, [Description ("J1 Balkone KrZi,Aufenth.Freien")]
        J1, [Description ("J2 Werkstst.Klinik,Beschäft-Th")]
        J2, [Description ("J3 Lagerräume/Archiv")]
        J3, [Description ("J4 Klimatechnik")]
        J4, [Description ("K1 Hörsäle,Unterrichtsräume")]
        K1, [Description ("P1 Fahrzeug/Abstellflächen")]
        P1, [Description ("R1 Reinraumber.(GMP-Labor etc)")]
        R1, [Description ("C3 Eingriff-Geburtshilfl.Räume")]
        C3, [Description ("Räume ohne zugeordn. NutzArt")]
        Default

    }

    public enum Raumnutzungsart {
        keine,
        Gemeinschaftsräume,
        [Description ("Aufenthaltsräume allgemein")]
        AufenthaltsräumeAllgemein,
        Bereitschaftsräume,
        Pausenräume,
        [Description ("Pausenräume allgemein")]
        PausenräumeAllgemein,
        [Description ("Ruheräume allgemein")]
        RuheräumeAllgemein,
        Patientenruheräume,
        Warteräume,
        [Description ("Warteräume allgemein")]
        WarteräumeAllgemein,
        Speiseräume,
        [Description ("Speiseräume allgemein")]
        SpeiseräumeAllgemein,
        Caféterias,
        Büroräume,
        [Description ("Büroräume allgemein")]
        BüroräumeAllgemein,
        Besprechungsräume,
        [Description ("Besprechungsräume allgemein")]
        BesprechungsräumeAllgemein,
        Sprechzimmer,
        Projekträume,
        [Description ("Technologische Labors")]
        TechnologischeLabors,
        Küchen,
        Lagerräume,
        [Description ("Lagerräume allgemein")]
        LagerräumeAllgemein,
        Kühlräume,
        Versorgungsstützpunkte,
        Entsorgungsstützpunkte,
        [Description ("Bildung,Unterricht u. Kultur")]
        Hörsaal,
        Übungsräume,
        [Description ("Räume für operative Eingriffe")]
        OperativeEingriffe,
        Operationsräume,
        [Description ("Röntgenuntersuchungsräume allg")]
        Radiologie,
        Normalpflegebettenräume,
        Infektionspflegebettenräume,
        Intensivbehandlung,
        [Description ("Aufwachräume (Postoperativ)")]
        Aufwachraum,
        [Description ("Sonstige Nutzungen")]
        Sonstiges,
        Sanitärräume,
        [Description ("Räume ohne zugeordnete NutzArt")]
        keineNutzungsart

    }

    public enum Funktionsbereich {
        Stationsbereich,
        [Description ("Untersuchung/Behandlung")]
        UB,
        [Description ("OP-Bereich")]
        OP,
        Verwaltungsbereich,
        [Description ("Sonstige Bereiche")]
        Sonstiges,
        Intensivbereich

    }

}