namespace APIAnimeHub.Models
{
    public enum AnimeStatus
    {
        Watching,
        Completed,
        PlanToWatch,
        Paused,
        Dropped
    }
}

//É uma lista fechada de valores possíveis.

//Sem enum:

//public string Status { get; set; }

//Você poderia salvar:

//Watching
//watching
//WATCHING
//watiching
//assistindo
//vendo

//Tudo isso seria aceito.

//Viraria bagunça.