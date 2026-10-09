namespace parcial_1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(
        IActivationState? activationState)
    {
        return new Window(new AppShell(
            Handler?.MauiContext?.Services
            ?? throw new InvalidOperationException(
                "No se encuentra disponible el contenedor de servicios.")));
    }
}