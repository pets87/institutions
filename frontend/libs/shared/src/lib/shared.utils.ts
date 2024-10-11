
export function getCurrentLanguage() {
  const currentUrlSegments = window.location.href.substring(window.location.origin.length + 1).split('/');
  return currentUrlSegments[ 0 ];
}

export function toDisplayDateTime(date?: Date | string): string | null {
  if (date == null)
    return null;

  let parsedDate: Date;
  
  if (typeof date === 'string') {
    parsedDate = new Date(date);
    if (isNaN(parsedDate.getTime())) {
      return null;
    }
  }
  // Check if input is already a Date object
  else if (date instanceof Date) {
    parsedDate = date;
  } else {
    return null;
  }

  const day = String(parsedDate.getDate()).padStart(2, '0');
  const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
  const year = parsedDate.getFullYear();

  const hours = String(parsedDate.getHours()).padStart(2, '0');
  const minutes = String(parsedDate.getMinutes()).padStart(2, '0');
  const seconds = String(parsedDate.getSeconds()).padStart(2, '0');

  return `${day}.${month}.${year} ${hours}:${minutes}:${seconds}`;
}
export function toDisplayDate(date?: Date | string): string | null {
  if (date == null)
    return null;

  let parsedDate: Date;

  if (typeof date === 'string') {
    parsedDate = new Date(date);
    if (isNaN(parsedDate.getTime())) {
      return null;
    }
  }
  // Check if input is already a Date object
  else if (date instanceof Date) {
    parsedDate = date;
  } else {
    return null;
  }

  const day = String(parsedDate.getDate()).padStart(2, '0');
  const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
  const year = parsedDate.getFullYear();

  return `${day}.${month}.${year}`;
}

