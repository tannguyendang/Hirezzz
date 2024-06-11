namespace Hirezzz.Models;
public abstract class BaseRepository
{
    protected MusicContext context;
    public BaseRepository(MusicContext context) => this.context = context;
}