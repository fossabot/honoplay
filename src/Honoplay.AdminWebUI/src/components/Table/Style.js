const Style = (theme) => ({
    root: {
      width: '100%',
      marginTop: theme.spacing.unit * 3,
    },
    table: {
      minWidth: 400,
    },
    tableWrapper: {
      overflowX: 'auto',
    },
    row: {
      '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.background.default,
      },
    },
    tableCell: {
      paddingRight: 3,
      paddingLeft:3,
      borderWidth: 1, 
      borderColor: '#dee2e6',
      borderStyle: 'solid',
      color: '#495057',
    },
    TableHead: {
      paddingRight: 4,
      paddingLeft:4,
      backgroundColor: '#e9ecef',
      color: '#495057',
      fontWeight: 'bold',
      borderWidth: 1, 
      borderColor: '#dee2e6',
      borderStyle: 'solid',
    },
    colorSwitchBase: {
      color: '#52d869',
      '&$colorChecked': {
        color: '#52d869',
        '& + $colorBar': {
          backgroundColor: '#52d869',
        },
      },
    },
    colorBar: {},
    colorChecked: {},
    icon: {
      margin: theme.spacing.unit,
      fontSize: 20,
    },
  });
  

  export default Style;