using Microsoft.Extensions.Logging;

namespace ConsoleApplication{
    public class WriteService : IWriteService{
        private readonly ILogger<WriteService> _logger;

        public WriteService(ILoggerFactory loggerFactory){
            _logger = loggerFactory.CreateLogger<WriteService>();
        }

        public void SaveResponse(string response){
            StreamWriter sw;
            try
            {
                if(File.Exists(StaticStrings.FileName)){
                    sw = File.AppendText(StaticStrings.FileName);
                }
                else{
                    sw = new StreamWriter(StaticStrings.FileName);
                }

                _logger.LogDebug("Saving a response");
                
                sw.WriteLine(response);
                sw.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}