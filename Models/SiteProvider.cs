namespace Hirezzz.Models;
public class SiteProvider : BaseProvider
{
    public SiteProvider(MusicContext context) : base(context)
    {
    }
    CategoryRepository category = null!;
    public CategoryRepository Category => category ??= new CategoryRepository(context);
    BannerRepository banner = null!;
    public BannerRepository Banner => banner ??= new BannerRepository(context);
    MemberRepository member = null!;
    public MemberRepository Member => member ??= new MemberRepository(context);
    ProductRepository product = null!;
    public ProductRepository Product => product ??= new ProductRepository(context);
    LibraryRepository library = null!;
    public LibraryRepository Library => library ??= new LibraryRepository(context);
    RoleRepository role = null!;
    public RoleRepository Role => role ??= new RoleRepository(context);
}