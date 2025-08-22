import { MasterMappingDataDto } from '../../models/DTO/Mapping/master-mapping-dto.model';

/**
 * The utilities helper class.
 */
export class Utilities {
  /**
   * Converts a Date object to a standardized yyyy-MM-dd string format.
   * Ensures consistent date formatting for comparison operations in filters.
   * @param date - The Date object to format
   * @returns A string representation of the date in yyyy-MM-dd format
   */
  public static formatDateToYMD(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  /**
   * Validates a member's name field.
   * @param name - The name string to validate
   * @returns True if name is between 2-100 characters, false otherwise
   */
  public static isValidName(name: string): boolean {
    return name !== '' && name.trim().length >= 2 && name.trim().length <= 100;
  }

  /**
   * Validates a member's email address using regex pattern.
   * @param email - The email string to validate
   * @returns True if email matches valid email format, false otherwise
   */
  public static isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return email !== '' && emailRegex.test(email.trim());
  }

  /**
   * Validates a member's phone number format.
   * @param phone - The phone number string to validate
   * @returns True if phone number contains exactly 10 digits, false otherwise
   */
  public static isValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^\d{10}$/;
    return phone !== '' && phoneRegex.test(phone.replace(/\s/g, ''));
  }

  /**
   * Validates a member's address field.
   * @param address - The address string to validate
   * @returns True if address is between 5-500 characters, false otherwise
   */
  public static isValidAddress(address: string): boolean {
    return (
      address !== '' &&
      address.trim().length >= 5 &&
      address.trim().length <= 500
    );
  }

  /**
   * Validates a member's gender selection.
   * @param gender - The gender string to validate
   * @returns True if gender is one of the allowed values (Male, Female, Other), false otherwise
   */
  public static isValidGender(gender: string): boolean {
    return gender !== '' && ['Male', 'Female', 'Other'].includes(gender);
  }

  /**
   * Validates a date object to ensure it's a valid Date instance.
   * @param date - The Date object to validate
   * @returns True if date is a valid Date object, false otherwise
   */
  public static isValidDate(date: Date): boolean {
    return date && date instanceof Date && !isNaN(date.getTime());
  }

  /**
   * Validates whether the provided master mapping data contains valid and usable information.
   * Checks for the presence of membership status mappings and other array-based mapping data.
   * Returns true if valid data exists, false otherwise.
   */
  public static checkValidMappingDataExists(
    data: MasterMappingDataDto
  ): boolean {
    return (
      data &&
      ((data.membershipStatusMapping &&
        data.membershipStatusMapping.length > 0) ||
        Object.keys(data).some((key) => {
          const value = (data as any)[key];
          return Array.isArray(value) && value.length > 0;
        }))
    );
  }

  /**
   * Gets the greeting based on time of the day.
   * @returns {string} The greeting
   */
  public static getGreeting(): string {
    const hour = new Date().getHours();

    if (hour < 12) return 'Morning';
    if (hour < 17) return 'Afternoon';
    return 'Evening';
  }

  /**
   * Gets the CSS class for the status chip based on the status value
   */
  public static getStatusChipClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active':
        return 'status-chip status-chip-active';
      case 'disabled':
        return 'status-chip status-chip-inactive';
      case 'decommissioned':
        return 'status-chip status-chip-pending';
      default:
        return 'status-chip status-chip-default';
    }
  }

  /**
   * Parses the markdown content table.
   * @param markdownContent The markdown content.
   * @returns The tupple containing the header and its rows.
   */
  public static parseMarkdownTable(
    markdownContent: string
  ): { headers: string[]; rows: string[][] } | null {
    const lines = markdownContent.trim().split('\n');
    const tableLines = lines.filter(
      (line) => line.trim().startsWith('|') && line.trim().endsWith('|')
    );

    if (tableLines.length < 3) return null;

    const headerLine = tableLines[0];
    const headers = headerLine
      .split('|')
      .slice(1, -1)
      .map((header) => header.trim());

    const dataLines = tableLines.slice(2);

    const rows = dataLines.map((line) =>
      line
        .split('|')
        .slice(1, -1)
        .map((cell) => cell.trim())
    );

    return { headers, rows };
  }
}
