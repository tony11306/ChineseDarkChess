using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseDarkChess {
    enum PieceEnum {
        BlackKing = 8,
        BlackGuard = 7,
        BlackElephant = 6,
        BlackRook = 5,
        BlackKnight = 4,
        BlackCannon = 3,
        BlackPawn = 2,
        RedKing = -8,
        RedGuard = -7,
        RedElephant = -6,
        RedRook = -5,
        RedKnight = -4,
        RedCannon = -3,
        RedPawn = -2,
        Unflip = 1,
        Empty = 0
    }
}
