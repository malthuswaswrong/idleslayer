namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class EnemyView : FrameView
{
    Label enemyName;
    Label enemyHp;
    ProgressBar enemyHealthBar;

    public EnemyView(): base("Enemy Info")
    {
        Width = Dim.Fill();
        Height = Dim.Fill();
        X = Pos.Center();
        Y = 0;
        var enemy = BattleEngine.CurrentEnemy;
        var enemyColorScheme = new ColorScheme();
        enemyColorScheme.Normal = new Terminal.Gui.Attribute(Color.Red, Color.Gray);
        enemyName = new Label($"Battling: {enemy.Name}") { Y = 0, X = Pos.Center() };
        enemyHp = new Label($"HP: {enemy.Health}") { Y = 1, X = Pos.Center() };

        enemyHealthBar = new ProgressBar()
        {
            X = Pos.Center(),
            Y = Pos.Bottom(enemyHp),
            Width = Dim.Fill(),
            Height = 1,
            Fraction = enemy.Health / enemy.HealthMax,
            ProgressBarStyle = ProgressBarStyle.Continuous,
            ColorScheme = enemyColorScheme,
        };

        Game.OnGameTick += HandleGameTick;
        Add(enemyName, enemyHp, enemyHealthBar);
    }
    ~EnemyView()
    {
        Game.OnGameTick -= HandleGameTick;
    }
    public void HandleGameTick(object? sender, EventArgs e)
    {
        var enemy = BattleEngine.CurrentEnemy;
        enemyName.Text = $"Battling: {enemy.Name}";
        enemyHp.Text = $"HP: {enemy.Health}";
        enemyHealthBar.Fraction = enemy.Health / enemy.HealthMax;
    }
}