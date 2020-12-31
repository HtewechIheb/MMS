
// Last Modified date : 31-05-2012 - 12:00

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyUtilities
{
    public static class FilesAccessLayer
    {
        /***********************************************************************************************************
         *                              DEROULEMENT DE L'INSERTION D'UN FICHIER
         * Dans le cas de l'ajout d'un nouveau fichier, l'interface d'édition des fichier permet de
         * travailler dans un fichier temporaire d'abord. Ce fichier est prédéfini et il doit être
         * déja existant dans le répertoire de travail (Exemple : TempResume.txt).
         * L'insertion réelle d'un nouveau fichier de texte doit accompagner l'execution de la requête 
         * SQL permettant d'insérer un enregistrement. Elle se fera dans ce cas avec la fonction 
         * InsertNewTxtFile. Cette dernière va dans ce cas copier le contenu du fichier temporaire dans un nouveau
         * fichier portant comme nom l'identifiant de l'enregistrement inséré.
         * *********************************************************************************************************/

        /***********************************************************************************************************
         *                              DEROULEMENT DE LA MISE A JOUR D'UN FICHIER  
         * La mise à jour d'un fichier se fait directement à travers l'interface d'édition des fichiers de texte.
         * Dans ce cas, le travail se fait directement sur le fichier associé à l'enregistrement en cours de traitement.
         * Ce fichier porte le nom de l'identifiant de l'enregistrement. Il n'y a aucun passage par un fichier temporaire
         * *********************************************************************************************************/

        public static void CreateTempFile(string FullTempFileName)
        {
            if (!File.Exists(FullTempFileName))
            {
                StreamWriter str = File.CreateText(FullTempFileName);
                str.Close();
            }
        }
        public static void InsertNewTxtFile(string FileServerPath, string ParamCote, string TempFileName)
        {
            string FullTempFileName = FileServerPath + TempFileName + ".txt"; ;
            string FullFileName = FileServerPath + ParamCote + ".txt";

            if (File.Exists(FullTempFileName))
            {
                File.Copy(FullTempFileName, FullFileName, true);
                File.Delete(FullTempFileName);
            }
        }
        public static string ReadTxtFile(string FullFileName)
        {
            if (File.Exists(FullFileName))
            {
                return File.ReadAllText(FullFileName);
            }
            else
                return "";
        }
        public static string ReadTempTxtFile(string TempFileName)
        {
            string Text = "";
            if (File.Exists(TempFileName))
            {
                Text = File.ReadAllText(TempFileName);
            }
            return Text;
        }
        public static void WriteTextToTempTxtFile(string TempFileName, string Text)
        {
            File.WriteAllText(TempFileName, Text);
        }
        public static void UpdateTxtFileContent(string FullFileName, string Text)
        {
            File.WriteAllText(FullFileName, Text);
        }
        public static void UpdateTxtFileName(string OldDocumentCote, string NewDocumentCote, string FileServerPath)
        {
            string OldFileFullfileName = FileServerPath + OldDocumentCote + ".txt";
            string NewFileFullfileName = FileServerPath + NewDocumentCote + ".txt";
            if ((OldDocumentCote != NewDocumentCote) && File.Exists(OldFileFullfileName))
            {
                File.Copy(OldFileFullfileName, NewFileFullfileName, true);
                File.Delete(OldFileFullfileName);
            }
        }
        public static void DeleteTxtFile(string ParamCote, string FileServerPath)
        {
            string FileFullFileName = FileServerPath + ParamCote + ".txt";

            if (File.Exists(FileFullFileName))
            {
                File.Delete(FileFullFileName);
            }
        }

    }
}

