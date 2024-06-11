namespace Hirezzz.Models;
public abstract class BaseProvider
{
    protected MusicContext context;
    public BaseProvider(MusicContext context) => this.context = context;
}