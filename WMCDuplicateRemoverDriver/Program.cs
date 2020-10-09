namespace WMCDuplicateRemoverDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var dryRun = args.Length > 0 && args[0] == "--dryrun";
            var driver = new Driver(dryRun);
            driver.Run();
        }
    }
}
