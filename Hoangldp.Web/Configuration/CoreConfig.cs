namespace Hoangldp.Core.Web.Configuration
{
    public class CoreConfig : ConfigBase
    {
        public override string SectionName => "core";

        public string LibLoadingPattern { get; set; }

        protected override void CreateConfig()
        {
            LibLoadingPattern = GetValueAttribute("lib_pattern", string.Empty);
        }
    }
}