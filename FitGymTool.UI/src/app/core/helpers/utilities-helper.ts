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
        (data.membershipStatusMapping &&
          data.membershipStatusMapping.length > 0) ||
        Object.keys(data).some((key) => {
          const value = (data as any)[key];
          return Array.isArray(value) && value.length > 0;
        }))
    );
  }

  public static getGreeting(): string {
    const hour = new Date().getHours();

    if (hour < 12) return 'Morning';
    if (hour < 17) return 'Afternoon';
    return 'Evening';
  }

  /**
   * Generates SVG path for status chart visualization.
   * @param isOnline - Whether the status is online/active
   * @param withVariation - Whether to add random variation to points
   * @returns SVG path string for the chart
   */
  public static generateStatusChartPath(
    isOnline: boolean,
    withVariation: boolean = false
  ): string {
    const basePoints = isOnline
      ? [
          { x: 0, y: 30 },
          { x: 15, y: 32 },
          { x: 30, y: 35 },
          { x: 45, y: 28 },
          { x: 60, y: 15 },
          { x: 75, y: 8 },
          { x: 90, y: 18 },
          { x: 105, y: 12 },
          { x: 120, y: 10 },
        ]
      : [
          { x: 0, y: 10 },
          { x: 15, y: 8 },
          { x: 30, y: 5 },
          { x: 45, y: 12 },
          { x: 60, y: 25 },
          { x: 75, y: 32 },
          { x: 90, y: 22 },
          { x: 105, y: 28 },
          { x: 120, y: 30 },
        ];

    const points = withVariation
      ? basePoints.map((point) => ({
          x: point.x,
          y: point.y + (Math.random() - 0.5) * 3,
        }))
      : basePoints;

    return this.createSmoothPath(points);
  }

  /**
   * Creates a smooth SVG path from an array of points using cubic Bezier curves.
   * @param points - Array of {x, y} coordinate points
   * @returns SVG path string
   */
  private static createSmoothPath(
    points: Array<{ x: number; y: number }>
  ): string {
    if (points.length === 0) return '';

    let path = `M ${points[0].x} ${points[0].y}`;

    for (let i = 1; i < points.length; i++) {
      const prevPoint = points[i - 1];
      const currentPoint = points[i];
      const controlPoint1 = {
        x: prevPoint.x + (currentPoint.x - prevPoint.x) * 0.4,
        y: prevPoint.y,
      };
      const controlPoint2 = {
        x: currentPoint.x - (currentPoint.x - prevPoint.x) * 0.4,
        y: currentPoint.y,
      };

      path += ` C ${controlPoint1.x} ${controlPoint1.y}, ${controlPoint2.x} ${controlPoint2.y}, ${currentPoint.x} ${currentPoint.y}`;
    }

    return path;
  }
}
