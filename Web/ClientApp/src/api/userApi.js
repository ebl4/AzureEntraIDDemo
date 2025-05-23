export async function fetchUserInfo(accessToken) {
  const response = await fetch('/api/user/info', {
    headers: {
      'Authorization': `Bearer ${accessToken}`
    }
  });
  
  if (!response.ok) {
    throw new Error('Failed to fetch user info');
  }
  
  return await response.json();
}