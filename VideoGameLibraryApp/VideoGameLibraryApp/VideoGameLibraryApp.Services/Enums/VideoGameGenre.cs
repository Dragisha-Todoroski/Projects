using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Services.Enums
{
    public enum VideoGameGenre
    {
        Action = 1,
        Adventure,
        Fighting,

        [Display(Name = "Grand Strategy")]
        GrandStrategy,
        Horror,

        [Display(Name = "Massively Multiplayer Online")]
        MassivelyMultiplayerOnline,
        Platformer,
        Puzzle,
        Racing,

        [Display(Name = "Real-Time Strategy")]
        RealTimeStrategy,
        Rhythm,

        [Display(Name = "Role-Playing")]
        RolePlaying,

        [Display(Name = "RogueLike/Lite")]
        RogueLikeAndRogueLite,
        Shooter,
        Simulation,

        [Display(Name = "Sports & Racing")]
        SportsAndRacing,
        Strategy,
        Survival,

        [Display(Name = "Turn-Based Strategy")]
        TurnBasedStrategy,
    }
}
