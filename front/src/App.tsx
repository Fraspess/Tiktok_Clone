

import './App.css'
import {type ILogin, useLoginMutation, useRefreshMutation} from "../store/apis/testApi.ts";

function App() {
  const [login] = useLoginMutation();
  const [refresh] = useRefreshMutation();

  const handleLogin = async () => {
    const testData : ILogin = {
      login: "admin@example.com",
      password:"Admin123!"
    };
    try{
      const response = await login(testData).unwrap();
      console.log(response);
    }
    catch (e) {
      console.error(e);
    }
  }
    const handleRefresh = async () => {
        try{
          const response = await refresh().unwrap();
          console.log(response);
        }
        catch (e) {
          console.error(e);
        }
    }
  return (
    <>
        <div style={{textAlign:"center"}}>
          <button style={{width: "50%", height: "30px"}} onClick={handleLogin}>Login</button>

        </div>
        <div style={{textAlign:"center"}}>
          <button style={{width: "50%", height: "30px", marginTop:"10px"}} onClick={handleRefresh}>Refresh</button>
        </div>

    </>
  )
}

export default App
