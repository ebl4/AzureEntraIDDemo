import React, { useState, useEffect } from 'react';
import { MsalProvider, useMsal, useIsAuthenticated } from '@azure/msal-react';
import { PublicClientApplication } from '@azure/msal-browser';
import { msalConfig, loginRequest } from './authConfig';
import { fetchUserInfo } from './api/userApi';

const msalInstance = new PublicClientApplication(msalConfig);

function App() {
  return (
    <MsalProvider instance={msalInstance}>
      <UserInfoComponent />
    </MsalProvider>
  );
}

function UserInfoComponent() {
  const { instance, accounts } = useMsal();
  const isAuthenticated = useIsAuthenticated();
  const [userInfo, setUserInfo] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (isAuthenticated && accounts.length > 0) {
      instance.acquireTokenSilent({
        ...loginRequest,
        account: accounts[0]
      }).then(response => {
        fetchUserInfo(response.accessToken)
          .then(data => setUserInfo(data))
          .catch(err => setError(err.message));
      }).catch(err => {
        setError(err.message);
      });
    }
  }, [isAuthenticated, accounts, instance]);

  const handleLogin = () => {
    instance.loginPopup(loginRequest)
      .catch(err => setError(err.message));
  };

  const handleLogout = () => {
    instance.logoutPopup();
    setUserInfo(null);
  };

  if (!isAuthenticated) {
    return (
      <div>
        <button onClick={handleLogin}>Login with Azure AD</button>
        {error && <p style={{ color: 'red' }}>{error}</p>}
      </div>
    );
  }

  return (
    <div>
      {userInfo ? (
        <div>
          <h2>User Information</h2>
          <p><strong>Name:</strong> {userInfo.name}</p>
          <p><strong>Email:</strong> {userInfo.email}</p>
          <p><strong>Tenant ID:</strong> {userInfo.tenantId}</p>
          <button onClick={handleLogout}>Logout</button>
        </div>
      ) : (
        <p>Loading user information...</p>
      )}
      {error && <p style={{ color: 'red' }}>{error}</p>}
    </div>
  );
}

export default App;