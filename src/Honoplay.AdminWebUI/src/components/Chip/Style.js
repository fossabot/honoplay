const Style = (theme) => ({
    chipRoot: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 2,
      height: 100
    },
    chip: {
      margin: theme.spacing.unit / 2,
    },
    chipTypography: {
      margin: theme.spacing.unit,
      color: '#495057',
      fontWeight: 'bold',
    },
  });

  export default Style; 