#r "nuget: Newtonsoft.Json"
open Newtonsoft.Json.Linq
open System
open System.IO

// aqui ficam as tabelas que vão gerar classes no projeto
let tabelas =
    [
        "DW_LOCADORA.FT_LOCACOES"
        "DW_LOCADORA.DM_ARTISTA"
        "DW_LOCADORA.DM_GRAVADORA"
        "DW_LOCADORA.DM_SOCIO"
        "DW_LOCADORA.DM_TEMPO"
        "DW_LOCADORA.DM_TITULO"
        "LOCADORA.GRAVADORAS"
        "LOCADORA.COPIAS"
        "LOCADORA.ARTISTAS"
        "LOCADORA.ITENS_LOCACOES"
        "LOCADORA.LOCACOES"
        "LOCADORA.SOCIOS"
        "LOCADORA.TIPOS_SOCIOS"
        "LOCADORA.TITULOS"
    ]

let caminho_appsettings = "EtlVendasProva.Processamento/appsettings.json"
let projeto_do_contexto = "EtlVendasProva.Data"
let nome_do_contexto = "VendasDwContext"
let diretorio_do_contexto = "Context"
let diretorio_das_entidades = "..\EtlVendasProva.Data\Domain\Entities\Dw"
let projeto_das_entidades = "EtlVendasProva.Data"
let caminho_string_conexao = "$.ConnectionStrings.VendasDwContext" 
let driver_banco_de_dados = "Npgsql.EntityFrameworkCore.PostgreSQL"

// Comandos do terminal
let restore = "dotnet restore"

let run str = 
    System.Diagnostics.Process.Start("CMD.exe","/C " + str).WaitForExit()

let addRef ref = "dotnet add " + projeto_do_contexto + " reference " + ref

let scaffold_str connection_string table_list =
    let table_str = table_list |> List.map(fun table -> " -t " + table) |> String.concat ""
    [ 
        "dotnet ef dbcontext scaffold \"" + connection_string + "\""
        driver_banco_de_dados
        "-v"
        "-f"
        "--context-dir " + diretorio_do_contexto
        "-c " + nome_do_contexto
        "-o " + diretorio_das_entidades
        "--no-onconfiguring"
        "--no-pluralize"
        "--project " + projeto_do_contexto
        //table_str
    ] |> String.concat " "

let addPackage pkg = "dotnet add " + projeto_do_contexto + " package " + pkg
//

let scaffold() =
    let appSettings = JToken.Parse(File.ReadAllText(caminho_appsettings))
    let conexao = appSettings.SelectToken(caminho_string_conexao).Value<string>()
    run <| scaffold_str conexao tabelas
    run <| addRef projeto_das_entidades

let install() =
    run <| addPackage "Microsoft.EntityFrameworkCore.Design"
    run <| addPackage "Microsoft.EntityFrameworkCore.Tools"
    run <| addPackage driver_banco_de_dados
    run restore
    printf "Pacotes instalados, gerar scaffold? (y/n) "
    let resposta = Console.ReadLine()
    if resposta = "y" then 
        scaffold()
    else
        ()

let rec main () =
    printf "Deseja instalar os pacotes para geracao automatica do scaffold? (y/n) "
    let resposta = String.map (Char.ToLower) (Console.ReadLine())

    match resposta with
    | "y" -> install()
    | "n" -> scaffold()
    | _   -> main()

main()
